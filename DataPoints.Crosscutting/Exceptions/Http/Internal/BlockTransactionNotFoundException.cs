using DataPoints.Crosscutting.Exceptions.Http.Internal.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.Internal;

public class BlockTransactionNotFoundException(Guid blockId) : InternalException(ErrorMessage.Exception.BlockTransactionNotFound(blockId))
{
    
}