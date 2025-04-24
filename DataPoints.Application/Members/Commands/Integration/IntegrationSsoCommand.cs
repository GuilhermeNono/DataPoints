using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Contract.Integration.Request;
using DataPoints.Contract.Integration.Response;

namespace DataPoints.Application.Members.Commands.Integration;

public record IntegrationSsoCommand(
    string Identifier,
    string FirstName,
    string LastName,
    string Phone,
    string Email,
    string DocumentNumber) : ICommand<IntegrationSsoResponse>
{
    public static IntegrationSsoCommand ToCommand(IntegrationSsoRequest request) => new (
        request.Identifier, request.FirstName, request.LastName, request.Phone, request.Email, request.DocumentNumber);
}