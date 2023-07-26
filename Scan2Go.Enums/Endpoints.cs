using Utility.Core.DataLayer;

namespace Scan2Go.Enums;

public class Endpoints : DataLayerEnumBase
{
    public const string endpointAddress = "https://localhost:44387/";
    #region Users

    public static readonly DataLayerEnumBase GetUsersList = new Endpoints("Users/GetUsersList");

    #endregion Users

    public Endpoints(string internalValue) : base(internalValue)
    {
        internalValue = endpointAddress + internalValue;
    }

    public static implicit operator string(Endpoints dataLayerEnumBase)
    {
        return dataLayerEnumBase.InternalValue;
    }
}