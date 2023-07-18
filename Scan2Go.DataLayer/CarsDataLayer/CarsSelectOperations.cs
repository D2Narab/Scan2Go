using Scan2Go.DataLayer.BaseClasses.SelectOperationBases;
using Scan2Go.Entity.Cars;
using Scan2Go.Enums;
using DataLayer.Base.DatabaseFactory;
using DataLayer.Base.SqlGenerator;
using Utility.Bases.EntityBases;

namespace Scan2Go.DataLayer.CarsDataLayer;
public class CarsSelectOperations : Scan2GoSelectOperations
{
	public CarsSelectOperations(DataLayerBase dataLayerBase) : base(dataLayerBase)
	{
	}

	public CriteriaResult GetCarsSearchDetailList(CarsSearchDetail carsSearchDetail)
	{
		SqlSelectFactory sqlSelectFactory = new SqlSelectFactory();

		sqlSelectFactory.SelectQuery.Append($" SELECT COUNT(*) OVER() AS { CriteriaResult.Field.TotalRecordCount} ,{ TableName.Cars.InternalValue}.*");
		sqlSelectFactory.FromQuery.Append($" FROM { TableName.Cars.InternalValue} ");
		sqlSelectFactory.WhereQuery.Append(" WHERE 1 = 1  ");
		sqlSelectFactory.OrderByQuery.Append($" ORDER BY { TableName.Cars.InternalValue}.{ Cars.Field.CarId}");
		sqlSelectFactory.OrderByQuery.Append($" { carsSearchDetail.SortType }");
		sqlSelectFactory.OrderByQuery.Append($" OFFSET { carsSearchDetail.StartFrom}  ROWS FETCH NEXT { carsSearchDetail.Range} ROWS ONLY OPTION(RECOMPILE)");

		return this.GetCriteriaResult(sqlSelectFactory);
	}
}
