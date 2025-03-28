using DataPoints.Crosscutting.Exceptions.Http.Internal.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.Internal;

public class DescriptionAttributeNotFoundException : InternalException
{
    public DescriptionAttributeNotFoundException() : base(ErrorMessage.Exception.DescriptionAttributeNotFound())
    {
    }
}