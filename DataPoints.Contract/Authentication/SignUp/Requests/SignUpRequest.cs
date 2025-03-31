namespace DataPoints.Contract.Authentication.SignUp.Requests;

public record SignUpRequest(
    string FirstName,
    string LastName,
    DateTime BirthDate,
    string DocumentNumber,
    string Email,
    string Password)
{
}
