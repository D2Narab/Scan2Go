using Utility.Core.DataLayer;

namespace Scan2Go.Enums;

public class CacheKey : DataLayerEnumBase
{
    public static readonly DataLayerEnumBase Users = new CacheKey("Users");

    public CacheKey(string internalValue) : base(internalValue)
    {
    }
}