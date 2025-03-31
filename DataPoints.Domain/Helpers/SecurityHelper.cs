using System.Security.Cryptography;
using System.Text;
using DataPoints.Domain.Objects;
using DataPoints.Domain.Objects.Security;
using NBitcoin;
using NBitcoin.Crypto;

namespace DataPoints.Domain.Helpers;

public static class SecurityHelper
{
    public static SecurityGroupKey CreateRsaKeys()
    {
        using var rsa = RSA.Create();

        var publicKey = rsa.ExportRSAPublicKey();
        var privateKey = rsa.ExportRSAPrivateKey();

        return new SecurityGroupKey(Convert.ToBase64String(publicKey), Convert.ToBase64String(privateKey));
    }

    public static SecurityGroupKey CreateEcdsaKeys()
    {
        var key = new Key();

        var publicKey = Convert.ToBase64String(key.PubKey.ToBytes());
        var privateKey = Convert.ToBase64String(key.ToBytes());

        return new SecurityGroupKey(publicKey, privateKey);
    }

    public static string SignTransaction(string data, string privateKeyBase64)
    {
        var key = new Key(Convert.FromBase64String(privateKeyBase64));    

        byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(data));
        ECDSASignature signature = key.Sign(new uint256(hash)); 

        return Convert.ToBase64String(signature.ToDER());
    }

    public static SecuritySha256Key CreateSha256Key(string baseOfHash)
    {
        byte[] hashInBytes = SHA256.HashData(Encoding.UTF8.GetBytes(baseOfHash));

        return new SecuritySha256Key(Convert.ToBase64String(hashInBytes));
    }

    public static bool IsValidPrivateKey(string privateKey)
    {
        try
        {
            var key = new Key(Convert.FromBase64String(privateKey));

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return key is not null;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsKeyPairValid(string publicKey, string privateKey)
    {
        try
        {
            var key = new Key(Convert.FromBase64String(privateKey));
            
            return Convert.ToBase64String(key.PubKey.ToBytes()) == publicKey;    
        }
        catch
        {
            return false;
        }
    }
}