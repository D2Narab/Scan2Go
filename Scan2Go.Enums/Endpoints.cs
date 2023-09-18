using Utility.Core.DataLayer;

namespace Scan2Go.Enums;

public class Endpoints : DataLayerEnumBase
{
    public const string EndpointAddress = "https://localhost:44387/";

    #region Cars

    public static readonly Endpoints CreateCar = new Endpoints("Cars/CreateCar");
    public static readonly Func<int, Endpoints> DeleteCar = new Func<int, Endpoints>(param => new Endpoints($"Cars/DeleteCar/{param}"));
    public static readonly Func<int, Endpoints> GetCar = new Func<int, Endpoints>(param => new Endpoints($"Cars/GetCar/{param}"));
    public static readonly Endpoints GetCarsList = new Endpoints("Cars/GetCarsList");
    public static readonly Endpoints SaveCar = new Endpoints("Cars/SaveCar");

    #endregion Cars

    #region Customers

    public static readonly Endpoints CreateCustomer = new Endpoints("Customers/CreateCustomer");
    public static readonly Func<int, Endpoints> DeleteCustomer = new Func<int, Endpoints>(param => new Endpoints($"Customers/DeleteCustomer/{param}"));
    public static readonly Func<int, Endpoints> GetCustomer = new Func<int, Endpoints>(param => new Endpoints($"Customers/GetCustomer/{param}"));
    public static readonly Endpoints GetCustomersList = new Endpoints("Customers/GetCustomersList");
    public static readonly Endpoints SaveCustomer = new Endpoints("Customers/SaveCustomer");

    #endregion Customers

    #region Rents

    public static readonly Endpoints CreateRent = new Endpoints("Rents/CreateRent");
    public static readonly Func<int, Endpoints> DeleteRent = new Func<int, Endpoints>(param => new Endpoints($"Rents/DeleteRent/{param}"));
    public static readonly Func<int, Endpoints> GetRent = new Func<int, Endpoints>(param => new Endpoints($"Rents/GetRent/{param}"));
    public static readonly Endpoints GetRentsList = new Endpoints("Rents/GetRentsList");
    public static readonly Endpoints SaveRent = new Endpoints("Rents/SaveRent");

    #endregion Rents

    #region Users

    public static readonly Endpoints CreateUser = new Endpoints("Users/CreateUser");
    public static readonly Func<int, Endpoints> DeleteUser = new Func<int, Endpoints>(param => new Endpoints($"Users/DeleteUser/{param}"));
    public static readonly Func<int, Endpoints> GetUser = new Func<int, Endpoints>(param => new Endpoints($"Users/GetUser/{param}"));
    public static readonly Endpoints GetUsersList = new Endpoints("Users/GetUsersList");
    public static readonly Endpoints SaveUser = new Endpoints("Users/SaveUser");

    #endregion Users

    private Endpoints(string internalValue) : base(EndpointAddress + internalValue)
    {
    }

    public static implicit operator string(Endpoints dataLayerEnumBase)
    {
        return dataLayerEnumBase.InternalValue;
    }
}