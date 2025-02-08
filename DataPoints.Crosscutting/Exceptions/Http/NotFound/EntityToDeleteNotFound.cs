using DataPoints.Crosscutting.Exceptions.Http.NotFound.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.NotFound;

public class EntityToDeleteNotFound : NotFoundException
{
    public EntityToDeleteNotFound(string entity) : base(ErrorMessage.Exception.EntityDeleteNotFoundException(entity))
    {
    }
}
