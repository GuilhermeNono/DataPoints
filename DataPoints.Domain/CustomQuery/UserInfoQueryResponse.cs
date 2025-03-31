using System.Text.Json.Serialization;

namespace DataPoints.Domain.CustomQuery;

public record UserInfoQueryResponse(Guid Id, string FirstName, string LastName, [property:JsonIgnore] string FullDocument, string WalletCode)
{
    public string Document => $"{FullDocument[..3]}.***.***-{FullDocument[^2..]}";
}