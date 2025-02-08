using DataPoints.Domain.Enums;

namespace DataPoints.Domain.Annotations;

/// <summary>
/// Essa anotação é responsável por sinalizar os casos de uso que não devem possuir nenhum tipo de transação.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class TransactionTypeAttribute : Attribute
{
    public readonly DbTransactionTypeEnum TransactionTypeEnum;

    public TransactionTypeAttribute(DbTransactionTypeEnum transactionTypeEnum = DbTransactionTypeEnum.ReadUncommitted)
    {
        TransactionTypeEnum = transactionTypeEnum;
    }
}
