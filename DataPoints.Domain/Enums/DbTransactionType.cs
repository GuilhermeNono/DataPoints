namespace DataPoints.Domain.Enums;

public enum DbTransactionType
{
    NoTransaction = 0,
    ReadCommit = 1,
    ReadUncommitted = 2,
    RepeatableRead = 3,
    Serializable = 4,
}
