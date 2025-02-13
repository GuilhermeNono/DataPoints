using System.ComponentModel;

namespace DataPoints.Domain.Enums;

public enum InternalOperation
{
    [Description("Create")]
    C,
    [Description("Update")]
    U,
    [Description("Delete")]
    D
}
