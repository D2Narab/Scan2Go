using Utility.Core.DataLayer;

namespace Scan2Go.Enums;

public class Endpoints : DataLayerEnumBase
{
    public const string endpointAddress = "https://localhost:44387/";

    #region Customers

    public static readonly Endpoints CreateCustomer = new Endpoints("Customers/CreateCustomer");
    public static readonly Func<int, Endpoints> DeleteCustomer = new Func<int, Endpoints>(param => new Endpoints($"Customers/DeleteCustomer/{param}"));
    public static readonly Func<int, Endpoints> GetCustomer = new Func<int, Endpoints>(param => new Endpoints($"Customers/GetCustomer/{param}"));
    public static readonly Endpoints GetCustomersList = new Endpoints("Customers/GetCustomersList");
    public static readonly Endpoints SaveCustomer = new Endpoints("Customers/SaveCustomer");

    #endregion Customers

    #region Users

    public static readonly Endpoints CreateUser = new Endpoints("Users/CreateUser");
    public static readonly Func<int, Endpoints> DeleteUser = new Func<int, Endpoints>(param => new Endpoints($"Users/DeleteUser/{param}"));
    public static readonly Func<int, Endpoints> GetUser = new Func<int, Endpoints>(param => new Endpoints($"Users/GetUser/{param}"));
    public static readonly Endpoints GetUsersList = new Endpoints("Users/GetUsersList");
    public static readonly Endpoints SaveUser = new Endpoints("Users/SaveUser");

    #endregion Users

    private Endpoints(string internalValue) : base(endpointAddress + internalValue)
    {
    }

    public static implicit operator string(Endpoints dataLayerEnumBase)
    {
        return dataLayerEnumBase.InternalValue;
    }
}
