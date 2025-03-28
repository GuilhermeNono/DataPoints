namespace DataPoints.Contract.Controller.Transaction.Insert.Request;

public record TransactionInsertRequest(string ReceiverWallet, decimal Amount)
{
    
}