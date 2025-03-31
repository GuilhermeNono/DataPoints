namespace DataPoints.Contract.Transaction.Insert.Request;

public record TransactionInsertRequest(string ReceiverWallet, decimal Amount)
{
    
}