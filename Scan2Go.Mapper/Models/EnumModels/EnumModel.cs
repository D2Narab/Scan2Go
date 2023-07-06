using Scan2Go.Enums;
using Scan2Go.Mapper.BaseClasses;
using Utility.Bases;

namespace Scan2Go.Mapper.Models.EnumModels;

public class EnumModel<T> : EnumModel, ISelectableItem
{
    public static implicit operator EnumModel<T>(T o)
    {
        if (o == null)
        {
            return new EnumModel<T>() { EnumId = 0, EnumName = string.Empty };
        }

        int enumId = (int)Enum.Parse(typeof(T), o.ToString());
        if (enumId == 0)
        {
            return null;
        }

        if (BaseMethods.user == null)
        {
            return new EnumModel<T>() { EnumId = (int)Enum.Parse(typeof(T), o.ToString()), EnumName = o.ToString() };
        }
        else
        {
            return new EnumModel<T>()
            {
                EnumId = (int)Enum.Parse(typeof(T), o.ToString()),
                EnumName = EnumMethods.GetResourceString(Enum.GetName(typeof(T), (int)Enum.Parse(typeof(T), o.ToString())), BaseMethods.user.GetLanguage())
            };
        }
    }

    public static implicit operator T(EnumModel<T> o)
    {
        if (o == null)
        {
            return default(T);
        }

        return (T)Enum.Parse(typeof(T), o.EnumId.ToString());
    }
}

public class EnumModel
{
    public int EnumId { get; set; }
    public string EnumName { get; set; }

    public int Id => EnumId;

    public string Label
    {
        get { return EnumName; }
    }

    public string Value
    {
        get { return EnumName; }
    }

    public EnumModel<T> createEnum<T>(string enumType)
    {
        Type type = enumType.GetEnumType();

        Type genericClass = typeof(EnumModel<>);
        // MakeGenericType is badly named
        Type constructedClass = genericClass.MakeGenericType(type);

        object created = Activator.CreateInstance(constructedClass);
        return (EnumModel<T>)created;
    }
}