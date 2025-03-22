using DataPoints.Crosscutting.Exceptions.Http.Internal.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.Internal;

public class BlockCorruptionException(Guid blockId) : InternalException(ErrorMessage.Exception.BlockCorruption(blockId))
{
    
}