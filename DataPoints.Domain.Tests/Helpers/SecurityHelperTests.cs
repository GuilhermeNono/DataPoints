using DataPoints.Domain.Helpers;
using Xunit;

namespace DataPoints.Domain.Tests.Helpers;

public class SecurityHelperTests
{
    [Fact]
    public void IsValidSignature_WithSignatureFromMatchingPrivateKey_ReturnsTrue()
    {
        var keys = SecurityHelper.CreateEcdsaKeys();
        const string canonicalPayload = "sender-hash|receiver-hash|10|nonce|2026-01-01T00:00:00Z";

        var signature = SecurityHelper.SignTransaction(canonicalPayload, keys.PrivateKey);

        Assert.True(SecurityHelper.IsValidSignature(canonicalPayload, signature, keys.PublicKey));
    }

    [Fact]
    public void IsValidSignature_WithTamperedPayload_ReturnsFalse()
    {
        var keys = SecurityHelper.CreateEcdsaKeys();
        const string canonicalPayload = "sender-hash|receiver-hash|10|nonce|2026-01-01T00:00:00Z";

        var signature = SecurityHelper.SignTransaction(canonicalPayload, keys.PrivateKey);

        Assert.False(SecurityHelper.IsValidSignature("sender-hash|receiver-hash|9999|nonce|2026-01-01T00:00:00Z",
            signature, keys.PublicKey));
    }

    [Fact]
    public void IsValidSignature_WithWrongPublicKey_ReturnsFalse()
    {
        var senderKeys = SecurityHelper.CreateEcdsaKeys();
        var attackerKeys = SecurityHelper.CreateEcdsaKeys();
        const string canonicalPayload = "sender-hash|receiver-hash|10|nonce|2026-01-01T00:00:00Z";

        var signature = SecurityHelper.SignTransaction(canonicalPayload, senderKeys.PrivateKey);

        Assert.False(SecurityHelper.IsValidSignature(canonicalPayload, signature, attackerKeys.PublicKey));
    }

    [Fact]
    public void IsValidSignature_WithGarbageSignature_ReturnsFalseInsteadOfThrowing()
    {
        var keys = SecurityHelper.CreateEcdsaKeys();

        Assert.False(SecurityHelper.IsValidSignature("payload", "not-a-real-signature", keys.PublicKey));
    }
}
