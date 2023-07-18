using Scan2Go.Entity.Cars;
using DataLayer.Base.SqlGenerator;

namespace Scan2Go.DataLayer.CarsDataLayer;
public class CarsSql
{
	public static SqlInsertFactory SaveCars(Cars cars)
	{
		SqlInsertFactory mainFactory = new SqlInsertFactory(cars);
		return mainFactory;
	}
}
