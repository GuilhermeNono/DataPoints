using System.Security.Cryptography;
using System.Text;
using DataPoints.Domain.Security.Transactions;

namespace DataPoints.Domain.Security;

public class Block
{
    public int Index { get; set; }
    public DateTime TimeSpan { get; set; }
    public List<Transaction> Transactions { get; set; }
    public string Data { get; set; }
    public string PreviousHash { get; set; }
    public string Hash { get; set; }
    public int Nonce { get; set; }

    public Block(int index, List<Transaction> transactions, string previousHash)
    {
        Index = index;
        TimeSpan = DateTime.UtcNow;
        Transactions = transactions;
        PreviousHash = previousHash;
        Nonce = 0;
        Hash = CalculateHash();
    }

    public string CalculateHash()
    {
        Data = $"{Index}-{TimeSpan}-{PreviousHash}-{Nonce}";
        foreach (var transaction in Transactions)
        {
            Data +=
                $"${transaction.Sender}->{transaction.Receiver}:{transaction.Amount}";
        }

        byte[] rawBytes = SHA256.HashData(Encoding.UTF8.GetBytes(Data));
        return BitConverter.ToString(rawBytes).Replace("-", "").ToLower();
    }

    public void MineBlock(int difficulty)
    {
        string target = new string('7', difficulty);
        while (!Hash.StartsWith(target))
        {
            Nonce++;
            Hash = CalculateHash();
        }
    }
}