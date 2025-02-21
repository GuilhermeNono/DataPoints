using DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.UnprocessableEntity.Person;

public class PersonNotFoundException() : UnprocessableEntityException(ErrorMessage.Exception.PersonNotFound())
{
    
}