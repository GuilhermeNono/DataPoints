using System.ComponentModel;
using System.Reflection;

namespace DataPoints.Domain.Helpers;

public class EnumHelper
{
    public static string GetDescription<TEnum>(TEnum @enum) where TEnum : notnull
    {
        var enumField = @enum?.GetType().GetField(@enum.ToString()!);
        
        var fieldInfo = (DescriptionAttribute?)enumField?.GetCustomAttribute(typeof(DescriptionAttribute), false);

        if (fieldInfo is null)
            throw new Exception($"Não foi possivel encontrar a anotação [Description] no tipo {enumField.Name}.");

        return fieldInfo.Description;
    }
}