using System.ComponentModel;

namespace DataPoints.Domain.Enums;

public enum ApiRouteVersion
{
    [Description("v1")]
    Version1,
    [Description("v2")]
    Version2,
    [Description("v3")]
    Version3
}