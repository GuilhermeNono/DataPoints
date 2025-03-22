using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Security.Cryptography;
using DataPoints.Domain.Database.Entity;

namespace DataPoints.Domain.Entities.Main;

[Table("Blc_Block")]
public class BlockEntity : Entity<Guid>
{
    public string Hash { get; set; } = string.Empty;
    public string PreviousHash { get; set; }
    public string MerkleRoot { get; set; }
    public bool IsValid { get; set; } = true;
    public DateTime DateInclusion { get; set; } = DateTime.UtcNow;

    public BlockEntity(string previousHash)
    {
        PreviousHash = previousHash;
    }

    public BlockEntity()
    {
    }

    public string CalculateHash()
    {
        string rawData = $"{Id}|{PreviousHash}|{DateInclusion}|MR={MerkleRoot}";
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
        return Hash = Convert.ToBase64String(bytes);
    }
    
    
}