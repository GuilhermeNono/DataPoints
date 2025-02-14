using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Entity;

public class EntityToDeleteNotFound : UnprocessableEntityException
{
    public EntityToDeleteNotFound(string entity) : base(ErrorMessage.Exception.EntityDeleteNotFoundException(entity))
    {
    }
}
