using Scan2Go.DataLayer.BaseClasses.DataLayersBases;
using Scan2Go.Entity.Customers;
using DataLayer.Base.DatabaseFactory;
using DataLayer.Base.GeneralDataLayer;

namespace Scan2Go.DataLayer.CustomersDataLayer;
internal class CustomersDMLOperations : Scan2GoDataLayerBase
{
	public  CustomersDMLOperations(DataLayerBase dataLayerBase) : base(dataLayerBase)
	{
	}

	internal void DeleteCustomers(Customers customers)
	{
		new GeneralDMLOperations(this).DeleteEntity(customers);
	}

	internal void SaveCustomers(Customers customers)
	{
		new GeneralDMLOperations(this).SaveEntity(customers);
	}
}
