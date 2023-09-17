using Scan2Go.Entity.Customers;
using DataLayer.Base.SqlGenerator;

namespace Scan2Go.DataLayer.CustomersDataLayer;
public class CustomersSql
{
	public static SqlInsertFactory SaveCustomers(Customers customers)
	{
		SqlInsertFactory mainFactory = new SqlInsertFactory(customers);
		return mainFactory;
	}
}
