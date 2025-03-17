namespace DataPoints.Domain.Security.Transactions;

public class Transaction
{
    public long Sender { get; set; }
    public long Receiver { get; set; }
    public long Amount { get; set; }

    public Transaction(long sender, long receiver, long amount)
    {
        Sender = sender;
        Receiver = receiver;
        Amount = amount;
    }
}
