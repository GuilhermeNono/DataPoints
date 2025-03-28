using DataPoints.Contract.Transaction.ByHash;
using DataPoints.Domain.Entities.Main;
using DataPoints.Domain.Enums;

namespace DataPoints.Crosscutting.Mapper.Responses;

public static class TransactionTreeMapper
{
    public static TransactionTreeResponse Map(this WalletTransactionEntity walletTransaction, PersonEntity to, PersonEntity from)
    {
        var transactionType = walletTransaction.IsCredit ? TransactionType.Credited : TransactionType.Debited;
            
        var userFrom = new TransactionDelivererResponse()
        {
            Id = from.Id,
            Name = from.FullName
        };

        var userTo = new TransactionDelivererResponse()
        {
            Id = to.Id,
            Name = to.FullName
        };

        return new TransactionTreeResponse
        {
            To = transactionType is TransactionType.Credited ? userTo : null,
            From = transactionType is TransactionType.Credited ? userFrom : userTo,
            Amount = walletTransaction.Amount,
            Type = transactionType
        };
    } 
}