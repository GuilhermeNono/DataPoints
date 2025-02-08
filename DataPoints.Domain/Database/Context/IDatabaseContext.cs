using DataPoints.Domain.Database.Transaction;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataPoints.Domain.Database.Context;

public interface IDatabaseContext : IDatabaseTransaction, IDisposable
{
    DatabaseFacade Database { get; }
}
