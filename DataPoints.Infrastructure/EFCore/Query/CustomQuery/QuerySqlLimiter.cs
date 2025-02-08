using DataPoints.Infrastructure.EFCore.Query.CustomQuery.Interfaces;

namespace DataPoints.Infrastructure.EFCore.Query.CustomQuery;

public class QuerySqlLimiter<TResult> : IQuerySqlLimiter<TResult>
{
    private const int Limiter = 25;

    public int LimitData { get; private set; } = Limiter;
    
    /// <summary>
    /// Este método deve ser utilizado para alterar o número do limitador
    /// de registros que uma consulta não paginada pode possuir.
    /// <param name="limitQuantity">Quantidade que será utilizada como novo limite</param>
    /// </summary>
    public void BypassLimiter(int limitQuantity) => LimitData = limitQuantity;
}