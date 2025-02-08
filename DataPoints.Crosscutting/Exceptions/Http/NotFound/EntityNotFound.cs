using DataPoints.Crosscutting.Exceptions.Http.NotFound.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.NotFound;

public class EntityNotFound : NotFoundException
{
    protected EntityNotFound(long id, string entityName) : base(ErrorMessage.Exception.EntityNotFound(id, entityName))
    {
    }  
    protected EntityNotFound(Guid id, string entityName) : base(ErrorMessage.Exception.EntityNotFound(id, entityName))
    {
    }

    public EntityNotFound(string? message) : base(message)
    {
    }
}
