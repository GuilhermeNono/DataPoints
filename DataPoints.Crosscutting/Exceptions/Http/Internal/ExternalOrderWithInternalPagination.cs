using DataPoints.Crosscutting.Exceptions.Http.Internal.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.Internal;

public class ExternalOrderWithInternalPagination : InternalException
{
    public ExternalOrderWithInternalPagination() : base(ErrorMessage.Exception
        .ExternalOrderWithInternalPagination())
    {
    }
}
