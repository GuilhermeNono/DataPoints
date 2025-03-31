namespace DataPoints.Contract.Users.Responses;

public record UserInfoResponse(string Id, string FirstName, string LastName, string Document, string WalletCode);