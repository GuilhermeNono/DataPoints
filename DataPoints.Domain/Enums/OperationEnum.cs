using System.ComponentModel;

namespace DataPoints.Domain.Enums;

public enum OperationEnum
{
    [Description("Create")]
    C,
    [Description("Update")]
    U,
    [Description("Delete")]
    D
}
