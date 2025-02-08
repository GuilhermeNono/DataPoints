namespace DataPoints.Domain.Annotations;

[AttributeUsage(AttributeTargets.Class)]
public class ViewAttribute : Attribute
{
    public readonly string Name;

    public ViewAttribute(string name)
    {
        Name = name;
    }
}