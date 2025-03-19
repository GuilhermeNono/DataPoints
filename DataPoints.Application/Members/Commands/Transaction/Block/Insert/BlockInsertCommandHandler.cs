using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Crosscutting.Exceptions.Http.NotFound;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Repositories.Main;

namespace DataPoints.Application.Members.Commands.Transaction.Block.Insert;

public class BlockInsertCommandHandler : ICommandHandler<BlockInsertCommand, string>
{
    
    private readonly IBlockRepository _blockRepository;
    private readonly IWalletTransactionRepository _walletTransactionRepository;

    public BlockInsertCommandHandler(IBlockRepository blockRepository, IWalletTransactionRepository walletTransactionRepository)
    {
        _blockRepository = blockRepository;
        _walletTransactionRepository = walletTransactionRepository;
    }

    public async Task<string> Handle(BlockInsertCommand request, CancellationToken cancellationToken)
    {
        var lastBlock = await _blockRepository.FindLastBlock()
            ?? throw new LastBlockNotFoundException();
        
        var transactions = (await _walletTransactionRepository.FindByIds(request.Transactions)).ToList();

        if (transactions.Count <= 0)
            throw new TransactionsNotFoundException();
        
        var newBlock = new BlockEntity(lastBlock.Hash);

        newBlock.CalculateHash(transactions);
        
        await _blockRepository.Add(newBlock, request.LoggedPerson.Name, cancellationToken);

        foreach (var transaction in transactions)
        {
            transaction.IdBlock = newBlock.Id;   
            await _walletTransactionRepository.Update(transaction, request.LoggedPerson.Name, cancellationToken);
        }
        
        return newBlock.Hash;
    }
}