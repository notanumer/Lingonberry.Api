using System.ComponentModel.DataAnnotations;

namespace Lingonberry.Api.UseCases.Handlers;

public static class DisplayEnum
{
    public static T GetValueFromName<T>(string name) where T : Enum
    {
        var type = typeof(T);

        foreach (var field in type.GetFields())
        {
            if (Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) is DisplayAttribute attribute)
            {
                if (attribute.Name == name)
                {
                    return (T)field.GetValue(null);
                }
            }

            if (field.Name == name)
            {
                return (T)field.GetValue(null);
            }
        }

        throw new ArgumentOutOfRangeException(nameof(name));
    }

    public static string GetValueFromEnum<T>(T name)
    {
        var type = typeof(T);

        foreach (var field in type.GetFields())
        {
            if (Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) is DisplayAttribute attribute)
            {
                return attribute.Name ?? null;
            }
        }

        throw new ArgumentOutOfRangeException(nameof(name));
    }
}
