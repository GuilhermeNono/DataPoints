using DataPoints.Crosscutting.Exceptions.Http.Internal.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.Internal;

public class DatabaseMigrationFailed : InternalException
{
    public DatabaseMigrationFailed() : base(ErrorMessage.Exception.MigrationFailed())
    {
    }
}
