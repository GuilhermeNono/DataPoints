using DataPoints.Application.Members.Abstractions.Queries;
using DataPoints.Contract.Transaction.ByHash;

namespace DataPoints.Application.Members.Queries.Transaction.ByHash;

public record TransactionGetByBlockHashQuery(Guid BlockId) : IQuery<IEnumerable<TransactionTreeResponse>>
{
    
}