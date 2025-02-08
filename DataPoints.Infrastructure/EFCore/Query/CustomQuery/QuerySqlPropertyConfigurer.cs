using System.Linq.Expressions;
using System.Text;

namespace DataPoints.Infrastructure.EFCore.Query.CustomQuery;

public class QuerySqlPropertyConfigurer<TResult>
{
    protected StringBuilder QueryBuilder = new ();

    /// <summary>
    /// Este método deve ser utilizado quando for necessário incluir um alias na consulta SQL para mapear um objeto.
    /// <typeparam name="TResult">Classe que será mapeada com os valores das consultas</typeparam>
    /// <typeparam name="TProperty">Propriedade que será utilizada no mapeamento</typeparam>
    /// <example>Exemplo:
    /// <code>
    /// $"SELECT ID {Alias(x => x.Identifier)}"
    /// </code>
    /// Se transforma em:
    /// <code>
    /// Select ID as Identifier
    /// </code>
    /// </example>
    /// <returns>Será retornado o nome da propriedade já mapeada com o alias <c>"as foo"</c>.</returns>
    /// </summary>
    protected string Alias<TProperty>(Expression<Func<TResult, TProperty>> expression)
    {
        return $"As {ClassProperty.PropertyName(expression)}";
    }
    
    /// <summary>
    /// Este método deve ser utilizado quando for necessário incluir um alias na consulta SQL para mapear um objeto.
    /// <param name="alias">Apelido que será utilizada na consulta</param>
    /// <example>Exemplo:
    /// <code>
    /// $"SELECT ID {Alias("Identifier")}"
    /// </code>
    /// Se transforma em:
    /// <code>
    /// Select ID as Identifier
    /// </code>
    /// </example>
    /// <returns>Será retornado o nome da propriedade já mapeada com o alias <c>"as foo"</c>.</returns>
    /// </summary>
    protected string Alias(string alias)
    {
        return $"As {alias}";
    }

    /// <summary>
    /// Este método deve ser utilizado quando for necessário incluir uma nova linha na consulta SQL do método <c>Prepare()</c>.
    /// <example>Exemplo:
    /// <code>
    /// Add("Select * ")
    /// Add("From foo")
    /// Add("Where bar = fuzz")
    /// </code>
    /// </example>
    /// </summary>
    protected void Add(string value)
    {
        Add(value, true);
    }

    /// <summary>
    /// Este método deve ser utilizado quando for necessário incluir uma nova linha na consulta SQL do método
    /// <c>Prepare()</c> com uma condição. Caso a condição seja falsa, o <c>add</c> é desconsiderado.
    /// <example>Exemplo:
    /// <code>
    /// Add("Select * ")
    /// Add("From foo")
    /// Add("Where bar = fuzz", false)
    /// </code>
    /// </example>
    /// </summary>
    protected void Add(string value, bool condition)
    {
        if (!condition)
            return;

        var containsTwoOrMoreWhereCondition = QueryBuilder.ToString().Contains("WHERE") && value.Contains("WHERE");

        if (containsTwoOrMoreWhereCondition)
            value = value.Replace("WHERE", "AND", StringComparison.InvariantCultureIgnoreCase);

        QueryBuilder.AppendLine(value);
    }
}