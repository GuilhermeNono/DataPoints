using System.Security.Cryptography;
using System.Text;
using DataPoints.Domain.Entities.Main;
using NBitcoin;
using NBitcoin.Crypto;

namespace DataPoints.Domain.Helpers;

public class BlockHelper
{
    public static string CalculateHash(BlockEntity block)
    {
        string rawData = $"{block.Id}|{block.PreviousHash}|{block.DateInclusion}|MR={block.MerkleRoot}";
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
        return Convert.ToBase64String(bytes);
    }

    public static bool IsValidBlockSignature(string hashBase64, string signatureBase64, string publicKeyBase64)
    {
        var hashBase = Hashes.SHA256(Encoding.UTF8.GetBytes(hashBase64));
        var signature = Convert.FromBase64String(signatureBase64);

        var pubKey = new PubKey(Convert.FromBase64String(publicKeyBase64));
        
        return pubKey.Verify(new uint256(hashBase), new ECDSASignature(signature));
    }
}