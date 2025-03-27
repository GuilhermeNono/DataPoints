using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Application.Members.Commands.Block.Validate;
using DataPoints.Crosscutting.Exceptions.Http.Internal;
using DataPoints.Crosscutting.Exceptions.Http.NotFound;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Helpers;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Domain.Security;
using MediatR;

namespace DataPoints.Application.Members.Commands.Block.Insert;

public class BlockInsertCommandHandler : ICommandHandler<BlockInsertCommand, string>
{
    private readonly IBlockRepository _blockRepository;
    private readonly IWalletTransactionRepository _walletTransactionRepository;
    private readonly ISender _sender;

    public BlockInsertCommandHandler(IBlockRepository blockRepository,
        IWalletTransactionRepository walletTransactionRepository, ISender sender)
    {
        _blockRepository = blockRepository;
        _walletTransactionRepository = walletTransactionRepository;
        _sender = sender;
    }

    public async Task<string> Handle(BlockInsertCommand request, CancellationToken cancellationToken)
    {
        var lastBlock = await _blockRepository.FindLastBlock()
                        ?? throw new LastBlockNotFoundException();

        var validatedBlock = await _sender.Send(new BlockValidateCommand(lastBlock.Id, request.LoggedPerson),
            cancellationToken);

        if (!validatedBlock.IsValid)
            throw new BlockCorruptionException(lastBlock.Id);

        var transactions = new List<WalletTransactionEntity>();

        foreach (var transaction in request.Transactions)
        {
            var walletTransaction = await _walletTransactionRepository.FindById(transaction);

            if (walletTransaction == null)
                continue;

            transactions.Add(walletTransaction);
        }

        if (transactions.Count <= 0)
            throw new TransactionsNotFoundException();

        var newBlock = new BlockEntity(lastBlock.Hash);

        var merkleRoot = MerkleTree.ComputeRoot(transactions.Select(x => x.TransactionSerialized).ToList());

        newBlock.MerkleRoot = merkleRoot;

        newBlock.Hash = BlockHelper.CalculateHash(newBlock);

        await _blockRepository.Add(newBlock, request.LoggedPerson.Name, cancellationToken);

        foreach (var transaction in transactions)
        {
            transaction.IdBlock = newBlock.Id;
            await _walletTransactionRepository.Update(transaction, request.LoggedPerson.Name, cancellationToken);
        }

        return newBlock.Hash;
    }
}