namespace DataPoints.Crosscutting.Configurations;

public interface IChainSigningConfiguration
{
    string PublicKey { get; set; }
    string PrivateKey { get; set; }
}
