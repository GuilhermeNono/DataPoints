namespace DataPoints.Infrastructure.EFCore.Query.CustomQuery.Interfaces;

public interface IQuerySqlConfigurer<TResult> 
{
    public string Count {get;}

    public string Query {get;}

    public QuerySqlLimiter<TResult> Limiter {get;}
}