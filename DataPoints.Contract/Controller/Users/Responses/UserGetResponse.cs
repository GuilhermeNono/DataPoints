namespace DataPoints.Contract.Controller.Users.Responses;

public class UserGetResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}