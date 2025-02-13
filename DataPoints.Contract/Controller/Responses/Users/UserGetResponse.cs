namespace DataPoints.Contract.Controller.Responses.Users;

public class UserGetResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}