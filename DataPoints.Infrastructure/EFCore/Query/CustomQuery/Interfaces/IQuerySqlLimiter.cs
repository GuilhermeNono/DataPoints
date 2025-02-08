namespace DataPoints.Infrastructure.EFCore.Query.CustomQuery.Interfaces;

public interface IQuerySqlLimiter<TResult>
{
    int LimitData {get;}

    void BypassLimiter(int limitQuantity);
}