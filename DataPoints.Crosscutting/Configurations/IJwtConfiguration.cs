namespace DataPoints.Crosscutting.Configurations;

public interface IJwtConfiguration
{
    string SecretKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpirationInMinutes { get; set; }
}