namespace DataPoints.Contract.Controller.Transaction.Insert.Request;

public record TransactionInsertRequest(string ReceiverPublicKey, decimal Amount)
{
    
}