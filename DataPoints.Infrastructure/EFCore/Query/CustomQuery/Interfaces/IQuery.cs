using System.Linq.Expressions;
using DataPoints.Domain.Enums;

namespace DataPoints.Infrastructure.EFCore.Query.CustomQuery.Interfaces;

public interface IQuery<TResult> : IBaseQuery<TResult>, IQuerySqlConfigurer<TResult> 
{
    public IQuery<TResult> OrderBy<TProperty>(Expression<Func<TResult, TProperty>> expression,
        Sort sort = Sort.Asc);

    public IQuery<TResult> OrderBy(string customOrder, Sort sort = Sort.Asc);
    public IQuery<TResult> PageConfig(int? pageSize, int? pageNumber);
}

public interface IQuery<TResult, TFilter> : IBaseQuery<TResult>, IQuerySqlConfigurer<TResult>
{
    public IQuery<TResult, TFilter> OrderBy<TProperty>(Expression<Func<TResult, TProperty>> expression,
        Sort sort = Sort.Asc);
    public IQuery<TResult, TFilter> OrderBy(string customOrder, Sort sort = Sort.Asc);
    public IQuery<TResult, TFilter> PageConfig(int? pageSize, int? pageNumber);
    public object[]? Parameters();
}
