using DataPoints.Crosscutting.Exceptions.Http.NotFound.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.NotFound;

public class TransactionsNotFoundException() : NotFoundException(ErrorMessage.Exception.TransactionsNotFound())
{
    
}