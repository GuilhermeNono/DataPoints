using DataPoints.Crosscutting.Messages;
using DataPoints.Domain.Errors.Exceptions;

namespace DataPoints.Crosscutting.Exceptions.Http.Internal;

public class ExternalOrderWithTreatablePagination : TreatableException
{
    public ExternalOrderWithTreatablePagination() : base(ErrorMessage.Exception
        .ExternalOrderWithInternalPagination())
    {
    }
}
