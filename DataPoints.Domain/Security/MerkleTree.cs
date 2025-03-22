using System.Security.Cryptography;
using System.Text;

namespace DataPoints.Domain.Security;

public class MerkleTree
{
    public static string ComputeRoot(IList<string> transactions)
    {
        if (transactions.Count == 0)
            throw new Exception("Não foi possivel prosseguir com a criação da Merkle Tree, pois não foram informados dados o suficiente.");
        
        List<string> hashes = new List<string>(transactions);
        
        while (hashes.Count > 1)
        {
            List<string> newLevels = new List<string>();

            for (int i = 0; i < hashes.Count; i += 2)
            {
                string left = hashes[i];
                string right = i + 1 < hashes.Count ? hashes[i + 1] : left;

                using (SHA256 sha256 = SHA256.Create())
                {
                    string combinedHashes = left + right;
                    byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedHashes));
                    newLevels.Add(Convert.ToHexString(hash));
                }
            }
            
            hashes = newLevels;
        }

        return hashes[0];
    }
}