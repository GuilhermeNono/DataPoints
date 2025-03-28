using System.Security.Cryptography;
using System.Text;
using DataPoints.Domain.Objects;
using DataPoints.Domain.Objects.Security;

namespace DataPoints.Domain.Helpers;

public static class SecurityHelper
{
    public static SecurityRsaKey CreateRsaKeys()
    {
        using var rsa = RSA.Create();
        
        var publicKey = rsa.ExportRSAPublicKey();
        var privateKey = rsa.ExportRSAPrivateKey();

        return new SecurityRsaKey(Convert.ToBase64String(publicKey), Convert.ToBase64String(privateKey));
    }

    public static SecuritySha256Key CreateSha256Key(string baseOfHash)
    {
        byte[] hashInBytes = SHA256.HashData(Encoding.UTF8.GetBytes(baseOfHash));
        
        return new SecuritySha256Key(Convert.ToBase64String(hashInBytes));
    }
}