using DataPoints.Crosscutting.Exceptions.Http.BadRequest.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.BadRequest;

public class DocumentCpfInvalidException() : BadRequestException(ErrorMessage.Exception.DocumentCpfInvalid())
{
    
}
