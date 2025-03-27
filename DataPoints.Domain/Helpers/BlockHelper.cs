using System.Security.Cryptography;
using System.Text;
using DataPoints.Domain.Entities.Main;

namespace DataPoints.Domain.Helpers;

public class BlockHelper
{
    public static string CalculateHash(BlockEntity block)
    {
        string rawData = $"{block.Id}|{block.PreviousHash}|{block.DateInclusion}|MR={block.MerkleRoot}";
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
        return Convert.ToBase64String(bytes);
    }
}