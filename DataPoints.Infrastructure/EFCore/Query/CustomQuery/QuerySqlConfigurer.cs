using DataPoints.Crosscutting.Exceptions;
using DataPoints.Crosscutting.Exceptions.Http.Internal;
using DataPoints.Domain.Database.Queries.Base;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery.Interfaces;

namespace DataPoints.Infrastructure.EFCore.Query.CustomQuery;

public abstract class QuerySqlConfigurer<TResult> : QuerySqlPropertyConfigurer<TResult>, IQuerySqlConfigurer<TResult>
{
    protected QuerySqlConfigurer()
    {
        Pagination = new Pagination<TResult>(null, null);
        Limiter = new QuerySqlLimiter<TResult>();
    }

    public QuerySqlLimiter<TResult> Limiter { get; }

    protected IPagination<TResult> Pagination { get; }
    private bool _disregardExternalOrder = false;

    /// <summary>
    /// Este método deve ser utilizado para construir a consulta SQL.
    /// <example>Exemplo:
    /// <code>
    /// Add("SELECT * ")
    /// Add("FROM foo_bar ")
    /// Add($"WHERE foo = {Param(x => x.bar)} ")
    /// </code>
    /// </example>
    /// </summary>
    protected abstract void Prepare();

    /// <summary>
    /// Essa propriedade deve ser utilizada para contagem dos registros de uma consulta.
    /// <returns>Será retornada a consulta SQL com a função agregada <c>"Count(1)"</c></returns>
    /// </summary>
    public string Count
    {
        get
        {
            QueryBuilder.Clear();
            SqlSelectWithCountQuery();
            return QueryBuilder.ToString();
        }
    }

    /// <summary>
    /// Essa propriedade deve ser utilizada para executar a consulta que foi desenvolvida no método <c>Prepare()</c>.
    /// <returns>Será retornada a consulta SQL</returns>
    /// </summary>
    public string Query
    {
        get
        {
            QueryBuilder.Clear();

            CheckIfThePaginationContainsOrdering();

            SqlSelectWithPrepareQuery();

            if (!_disregardExternalOrder)
            {
            }

            SqlOrderingQuery();

            SqlPaginationQuery();

            return QueryBuilder.ToString();
        }
    }

    private void SqlSelectWithCountQuery()
    {
        QueryBuilder.Clear();
        Add(" Select Count(1) as Value");
        Add("   From ( ");
        Prepare();
        if (QueryBuilder.ToString().Contains("order by", StringComparison.InvariantCultureIgnoreCase))
            _disregardExternalOrder = true;
        Add("        ) t ");
    }

    private void SqlSelectWithPrepareQuery()
    {
        QueryBuilder.Clear();
        Add($"   Select {SqlDataLimiter()} * ");
        Add("   From ( ");
        Prepare();
        Add("        ) t ");
    }

    private void SqlPaginationQuery()
    {
        Pagination.Validate();

        if (!(Pagination?.IsPageable ?? false))
            return;

        if (_disregardExternalOrder)
            throw new ExternalOrderWithTreatablePagination();

        Add($"  Offset {Pagination?.Size ?? 10} * ({Pagination?.Page} - 1) ");
        Add($"  Rows Fetch Next {Pagination?.Size} Rows Only");
    }

    private void SqlOrderingQuery()
    {
        Add($"  Order By ", Pagination.IsSortable);
        foreach (var order in Pagination.Ordering)
        {
            Add($" {order?.Column} {order?.Sort} ", Pagination.IsSortable);

            if (!Pagination.IsLastInOrder(order!))
                Add(", ", Pagination.IsSortable);
        }
    }

    private string SqlDataLimiter()
    {
        return Pagination.Size == 0 ? $"TOP({Limiter.LimitData})" : string.Empty;
    }

    private void CheckIfThePaginationContainsOrdering()
    {
        if (Pagination is { IsPageable: true, IsSortable: false })
            throw new ArgumentException($"Order property not assigned in class {GetType().Name}.");
    }
}
