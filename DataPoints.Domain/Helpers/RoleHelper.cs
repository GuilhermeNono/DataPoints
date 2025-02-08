namespace DataPoints.Domain.Helpers;

public static class RoleHelper
{
    public const string Administrator = "ADMINISTRATOR";
    public const string Client = "CLIENT";
    public const string User = "USER";

    public const string HighPrivileges = $" {Administrator}, {Client} ";
}
