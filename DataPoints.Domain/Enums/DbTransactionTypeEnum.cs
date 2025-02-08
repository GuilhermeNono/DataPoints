namespace DataPoints.Domain.Enums;

public enum DbTransactionTypeEnum
{
    NoTransaction = 0,
    ReadCommit = 1,
    ReadUncommitted = 2,
}
