using DataPoints.Crosscutting.Messages;
using DataPoints.Domain.Errors.Exceptions;

namespace DataPoints.Crosscutting.Exceptions.Http.Internal;

public class DatabaseMigrationFailed : TreatableException
{
    public DatabaseMigrationFailed() : base(ErrorMessage.Exception.MigrationFailed())
    {
    }
}
