using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Entity;

public class EntityNotFound : UnprocessableEntityException
{
    protected EntityNotFound(long id, string entityName) : base(ErrorMessage.Exception.EntityNotFound(id, entityName))
    {
    }  
    protected EntityNotFound(Guid id, string entityName) : base(ErrorMessage.Exception.EntityNotFound(id, entityName))
    {
    }

    public EntityNotFound(string message) : base(message)
    {
    }
}
