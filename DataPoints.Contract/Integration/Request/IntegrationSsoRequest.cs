namespace DataPoints.Contract.Integration.Request;

public record IntegrationSsoRequest(
    string Identifier,
    string FirstName,
    string LastName,
    string Phone,
    string Email,
    string DocumentNumber)
{
}