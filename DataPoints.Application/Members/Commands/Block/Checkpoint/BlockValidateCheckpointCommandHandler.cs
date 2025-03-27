using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Application.Members.Commands.Block.Checkpoint.Clean;
using DataPoints.Application.Members.Commands.Block.Validate;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Enums.Entities;
using DataPoints.Domain.Repositories.Main;
using MediatR;

namespace DataPoints.Application.Members.Commands.Block.Checkpoint;

public class BlockValidateCheckpointCommandHandler : ICommandHandler<BlockValidateCheckpointCommand>
{
    private readonly IBatchValidationRepository _batchValidationRepository;
    private readonly IBatchCheckpointRepository _batchCheckpointRepository;
    private readonly IBlockRepository _blockRepository;
    private readonly ISender _sender;

    private const int QuantityOfBlocksToValidate = 10;

    public BlockValidateCheckpointCommandHandler(IBatchValidationRepository batchValidationRepository,
        IBlockRepository blockRepository, ISender sender, IBatchCheckpointRepository batchCheckpointRepository)
    {
        _batchValidationRepository = batchValidationRepository;
        _blockRepository = blockRepository;
        _sender = sender;
        _batchCheckpointRepository = batchCheckpointRepository;
    }

    public async Task Handle(BlockValidateCheckpointCommand request, CancellationToken cancellationToken)
    {
        BatchValidationEntity? latestValidation = await _batchValidationRepository.FindLatestValidationAvailable();

        if (latestValidation is null)
            latestValidation = await _batchValidationRepository.Add(new BatchValidationEntity(),
                request.LoggedPerson.Name, cancellationToken);

        var blocksToValidate = (await _blockRepository.FindNonValidated(latestValidation.Id,
            QuantityOfBlocksToValidate)).ToList();
        
        if(latestValidation.IdBatchStatus is BatchStateType.Pending)
            latestValidation.IdBatchStatus = BatchStateType.Processing;

        if (blocksToValidate.Count <= 0)
        {
            latestValidation.EndValidation = DateTime.Now;
            latestValidation.IdBatchStatus = BatchStateType.Completed;
            
            await _sender.Send(new BlockCleanCheckpointCommand(latestValidation.Id, request.LoggedPerson), cancellationToken);
        }

        foreach (var block in blocksToValidate)
        {
            var blockValidated = await _sender.Send(new BlockValidateCommand(block.Id, request.LoggedPerson),
                cancellationToken);

            var blockCheckpoint = await _batchCheckpointRepository.Add(new BatchCheckpointEntity
            {
                IdBatch = latestValidation.Id,
                IdBlock = block.Id
            }, request.LoggedPerson.Name, cancellationToken);


            if (!blockValidated.IsValid)
            {
                InvalidateBlock(latestValidation);

                blockCheckpoint.IsValid = false;
                await _batchCheckpointRepository.Update(blockCheckpoint, request.LoggedPerson.Name, cancellationToken);
                continue;
            }

            ProcessBlock(latestValidation);
        }
        
        await _batchValidationRepository.Update(latestValidation, request.LoggedPerson.Name, cancellationToken);
    }

    private static void ProcessBlock(BatchValidationEntity batch) =>
        batch.BlockProcessed += 1;

    private static void InvalidateBlock(BatchValidationEntity batch)
    {
        ProcessBlock(batch);
        batch.BlockInvalidated += 1;
    }
}