using System.Diagnostics.CodeAnalysis;
using DataPoints.Domain.Enums;
using DataPoints.Domain.Helpers;
using Microsoft.AspNetCore.Mvc.Routing;

namespace DataPoints.Domain.Annotations;

[AttributeUsage(AttributeTargets.Class)]
public class ApiRouteAttribute : Attribute, IRouteTemplateProvider
{
    [StringSyntax("Route")]
    public string? Template { get; }

    private int? _order;
    public int Order
    {
        get => _order ?? 0;
        set => _order = value;
    }

    /// <inheritdoc />
    int? IRouteTemplateProvider.Order => _order;

    /// <inheritdoc />
    public string? Name { get; set; }

    public ApiRouteAttribute([StringSyntax("Route")] string? route, ApiRouteVersion version = ApiRouteVersion.Version1)
    {
        Template = $"{EnumHelper.GetDescription(version)}/{route}";
    }
}