using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Crosscutting.Exceptions.Http.Internal;
using DataPoints.Domain.Repositories.Main;

namespace DataPoints.Application.Members.Commands.Block.Checkpoint.Clean;

public class BlockCleanCheckpointCommandHandler : ICommandHandler<BlockCleanCheckpointCommand>
{
    
    private readonly IBatchCheckpointRepository _batchCheckpointRepository;

    public BlockCleanCheckpointCommandHandler(IBatchCheckpointRepository batchCheckpointRepository)
    {
        _batchCheckpointRepository = batchCheckpointRepository;
    }

    public async Task Handle(BlockCleanCheckpointCommand request, CancellationToken cancellationToken)
    {
        var checkpoints = (await _batchCheckpointRepository.FindByBatch(request.ValidationId, false)).ToList();

        foreach (var checkpoint in checkpoints)
            await _batchCheckpointRepository.Delete(checkpoint, cancellationToken);
    }
}