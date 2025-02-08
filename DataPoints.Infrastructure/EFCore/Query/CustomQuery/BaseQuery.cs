using System.Collections;
using System.Linq.Expressions;
using DataPoints.Domain.Annotations;
using DataPoints.Domain.Enums;
using DataPoints.Infrastructure.EFCore.Query.CustomQuery.Interfaces;
using Microsoft.Data.SqlClient;

namespace DataPoints.Infrastructure.EFCore.Query.CustomQuery;

public abstract class BaseQuery<TResult> : QuerySqlConfigurer<TResult>, IQuery<TResult>

{
    public bool IsCountable => Pagination.IsPageable;

    /// <summary>
    /// Este método deve ser utilizado quando for necessário incluir uma nova ordenação à consulta SQL.
    /// Se o objeto for paginado, este método é obrigatório para o funcionamento da consulta.
    /// <typeparam name="TResult">Classe que será utilizada para definir quais propriedades serão ordenadas</typeparam>
    /// <typeparam name="TProperty">Propriedade do objeto a ser utilizada na ordenação</typeparam>
    /// <example>Exemplo:
    /// <code>
    /// OrderBy(x => x.Id, Sort.DESC)
    /// </code>
    /// </example>
    /// <returns>Será retornada a instância de IQuery para que você possa continuar a configurar a consulta.</returns>
    /// </summary>
    public IQuery<TResult> OrderBy<TProperty>(Expression<Func<TResult, TProperty>> expression, Sort sort = Sort.Asc)
    {
        Pagination.OrderBy(expression, sort);
        return this;
    }

    /// <summary>
    /// Este método deve ser utilizado quando for necessário incluir uma nova ordenação à consulta SQL.
    /// Se o objeto for paginado, este método é obrigatório para o funcionamento da consulta.
    /// <typeparam name="TResult">Classe que será utilizada para definir quais propriedades serão ordenadas</typeparam>
    /// <example>Exemplo:
    /// <code>
    /// OrderBy("IIF(Position is null, 1, 0), Position", Sort.ASC)
    /// </code>
    /// </example>
    /// <returns>Será retornada a instância de IQuery para que você possa continuar a configurar a consulta.</returns>
    /// </summary>
    public IQuery<TResult> OrderBy(string customOrder, Sort sort = Sort.Asc)
    {
        Pagination.OrderBy(customOrder, sort);
        return this;
    }

    /// <summary>
    /// Este método deve ser utilizado quando for necessário informar o
    /// tamanho de registros por página e a página em que os dados serão procurados.
    /// <example>Exemplo:
    /// <code>
    /// PageConfig(10, 1);
    /// </code>
    /// </example>
    /// <returns>Será retornada a instância de IQuery para que você possa continuar a configurar a consulta.</returns>
    /// </summary>
    public IQuery<TResult> PageConfig(int? pageSize, int? pageNumber)
    {
        Pagination.Size = pageSize;
        Pagination.Page = pageNumber;
        return this;
    }
}

public abstract class BaseQuery<TResult, TFilter>(TFilter filter)
    : QuerySqlConfigurer<TResult>, IQuery<TResult, TFilter>
{
    protected TFilter Filter { get; } = filter;
    public bool IsCountable => Pagination.IsPageable;

    /// <summary>
    /// Este método deve ser utilizado quando for necessário incluir uma nova ordenação à consulta SQL.
    /// Se o objeto for paginado, este método é obrigatório para o funcionamento da consulta.
    /// <typeparam name="TResult">Classe que será utilizada para definir quais propriedades serão ordenadas</typeparam>
    /// <typeparam name="TProperty">Propriedade do objeto a ser utilizada na ordenação</typeparam>
    /// <example>Exemplo:
    /// <code>
    /// OrderBy(x => x.Id, Sort.DESC)
    /// </code>
    /// </example>
    /// <returns>Será retornada a instância de IQuery para que você possa continuar a configurar a consulta.</returns>
    /// </summary>
    public IQuery<TResult, TFilter> OrderBy<TProperty>(Expression<Func<TResult, TProperty>> expression,
        Sort sort = Sort.Asc)
    {
        Pagination.OrderBy(expression, sort);
        return this;
    }

    public IQuery<TResult, TFilter> OrderBy(string customOrder, Sort sort = Sort.Asc)
    {
        Pagination.OrderBy(customOrder, sort);
        return this;
    }

    /// <summary>
    /// Este método deve ser utilizado quando for necessário informar o
    /// tamanho de registros por página e a página em que os dados serão procurados.
    /// <example>Exemplo:
    /// <code>
    /// PageConfig(10, 1);
    /// </code>
    /// </example>
    /// <returns>Será retornada a instância de IQuery para que você possa continuar a configurar a consulta.</returns>
    /// </summary>
    public IQuery<TResult, TFilter> PageConfig(int? pageSize, int? pageNumber = 0)
    {
        Pagination.Size = pageSize;
        Pagination.Page = pageNumber;
        return this;
    }

    public object[]? Parameters()
    {
        CheckIfTheFilterIsNull(Filter);

        var filterParameter = FilterPropertiesToSqlParameters()?.ToArray();
        return filterParameter;
    }

    private static void CheckIfTheFilterIsNull(TFilter filter)
    {
        if (filter == null)
            throw new ArgumentNullException(nameof(filter),
                "O campo filter não pode ser nulo para consultas com filtros");
    }

    private IEnumerable<object>? FilterPropertiesToSqlParameters()
    {
        if (Filter == null)
            return null;

        var parameters = new List<SqlParameter>();

        foreach (var property in Filter.GetType().GetProperties())
        {
            var value = property.GetValue(Filter);

            var isIgnorable = Attribute.IsDefined(property, typeof(IgnoreFilterPropertyAttribute));

            if(isIgnorable)
                continue;

            if (value == null)
                parameters.Add(new SqlParameter(property.Name, DBNull.Value));

            if (value is IDictionary dictionary)
            {
                List<SqlParameter>? dicValues = null;

                foreach (DictionaryEntry entry in dictionary)
                {
                    if (entry.Value is IList list)
                    {
                        foreach (var item in list)
                        {
                            dicValues ??= new List<SqlParameter>();
                            dicValues.Add(new SqlParameter(entry.Key.ToString(), item));
                        }
                    }
                }

                dicValues ??=
                    ((Dictionary<object, object>)dictionary).Select(
                        x => new SqlParameter(x.Key.ToString(), x.Value)).ToList();

                parameters.AddRange(dicValues);
            }
            else if (!(value is string) && value is IEnumerable enumerable)
            {
                foreach (var item in enumerable)
                {
                    parameters.Add(new SqlParameter(property.Name, item ?? DBNull.Value));
                }
            }
            else
            {
                parameters.Add(new SqlParameter(property.Name, value));
            }
        }

        return parameters.Any() ? parameters : null;
    }
}
