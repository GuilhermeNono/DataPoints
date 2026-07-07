using DataPoints.Domain.Security;
using Xunit;

namespace DataPoints.Domain.Tests.Security;

public class MerkleTreeTests
{
    [Fact]
    public void ComputeRoot_WithSingleTransaction_ReturnsHashOfItself()
    {
        var root = MerkleTree.ComputeRoot(["tx-1"]);

        Assert.NotNull(root);
        Assert.NotEmpty(root);
    }

    [Fact]
    public void ComputeRoot_IsDeterministic_ForSameInput()
    {
        var first = MerkleTree.ComputeRoot(["tx-1", "tx-2", "tx-3"]);
        var second = MerkleTree.ComputeRoot(["tx-1", "tx-2", "tx-3"]);

        Assert.Equal(first, second);
    }

    [Fact]
    public void ComputeRoot_ChangesWhenAnyTransactionChanges()
    {
        var original = MerkleTree.ComputeRoot(["tx-1", "tx-2", "tx-3"]);
        var tampered = MerkleTree.ComputeRoot(["tx-1", "tx-2", "tx-3-tampered"]);

        Assert.NotEqual(original, tampered);
    }

    [Fact]
    public void ComputeRoot_WithEmptyList_Throws()
    {
        Assert.Throws<Exception>(() => MerkleTree.ComputeRoot([]));
    }
}
