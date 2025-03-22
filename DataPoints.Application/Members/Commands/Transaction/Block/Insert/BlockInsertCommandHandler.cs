using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Crosscutting.Exceptions.Http.Internal;
using DataPoints.Crosscutting.Exceptions.Http.NotFound;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;
using DataPoints.Domain.Security;

namespace DataPoints.Application.Members.Commands.Transaction.Block.Insert;

public class BlockInsertCommandHandler : ICommandHandler<BlockInsertCommand, string>
{
    private readonly IBlockRepository _blockRepository;
    private readonly IWalletTransactionRepository _walletTransactionRepository;

    public BlockInsertCommandHandler(IBlockRepository blockRepository,
        IWalletTransactionRepository walletTransactionRepository)
    {
        _blockRepository = blockRepository;
        _walletTransactionRepository = walletTransactionRepository;
    }

    public async Task<string> Handle(BlockInsertCommand request, CancellationToken cancellationToken)
    {
        var lastBlock = await _blockRepository.FindLastBlock()
                        ?? throw new LastBlockNotFoundException();

        var lastBlockTransactions = (await _walletTransactionRepository.FindByIdBlock(lastBlock.Id)).ToList();

        if (lastBlockTransactions.Count == 0)
            throw new BlockTransactionNotFoundException(lastBlock.Id);

        var calculateMerkleRootFromLastBlock =
            MerkleTree.ComputeRoot(lastBlockTransactions.Select(x => x.TransactionSerialized).ToList());

        if (!lastBlock.Hash.Equals("GenesisBlock", StringComparison.CurrentCultureIgnoreCase) &&
            !calculateMerkleRootFromLastBlock.Equals(lastBlock.MerkleRoot, StringComparison.CurrentCultureIgnoreCase))
        {
            lastBlock.IsValid = false;
            throw new BlockCorruptionException(lastBlock.Id);
        }

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

        newBlock.CalculateHash();

        await _blockRepository.Add(newBlock, request.LoggedPerson.Name, cancellationToken);

        foreach (var transaction in transactions)
        {
            transaction.IdBlock = newBlock.Id;
            await _walletTransactionRepository.Update(transaction, request.LoggedPerson.Name, cancellationToken);
        }

        return newBlock.Hash;
    }
}