using DataPoints.Crosscutting.Exceptions.Http.NotFound.Abstractions;

namespace DataPoints.Crosscutting.Exceptions.Http.NotFound;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException() : base("Usuario não encontrado")
    {
    }
}