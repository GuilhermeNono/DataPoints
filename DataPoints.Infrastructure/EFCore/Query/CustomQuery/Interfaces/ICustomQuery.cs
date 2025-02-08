namespace DataPoints.Infrastructure.EFCore.Query.CustomQuery.Interfaces;

public interface ICustomQuery<TResult> : IQuery<TResult> 
{
}

public interface ICustomQuery<TFilter, TResult> :  IQuery<TResult, TFilter> 
{
}