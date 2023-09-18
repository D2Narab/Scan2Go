using Scan2Go.Entity.Rents;
using DataLayer.Base.SqlGenerator;

namespace Scan2Go.DataLayer.RentsDataLayer;
public class RentsSql
{
	public static SqlInsertFactory SaveRents(Rents rents)
	{
		SqlInsertFactory mainFactory = new SqlInsertFactory(rents);
		return mainFactory;
	}
}
