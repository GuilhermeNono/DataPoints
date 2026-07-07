using DataPoints.Crosscutting.Configurations;

namespace DataPoints.Api.Configurations;

public class ChainSigningConfiguration : IChainSigningConfiguration
{
    public string PublicKey { get; set; } = string.Empty;
    public string PrivateKey { get; set; } = string.Empty;
}
