using Scan2Go.DataLayer.BaseClasses.SelectOperationBases;
using Scan2Go.Entity.Rents;
using Scan2Go.Enums;
using DataLayer.Base.DatabaseFactory;
using DataLayer.Base.SqlGenerator;
using Utility.Bases.EntityBases;

namespace Scan2Go.DataLayer.RentsDataLayer;
public class RentsSelectOperations : Scan2GoSelectOperations
{
	public RentsSelectOperations(DataLayerBase dataLayerBase) : base(dataLayerBase)
	{
	}

	public CriteriaResult GetRentsSearchDetailList(RentsSearchDetail rentsSearchDetail)
	{
		SqlSelectFactory sqlSelectFactory = new SqlSelectFactory();

		sqlSelectFactory.SelectQuery.Append($" SELECT COUNT(*) OVER() AS { CriteriaResult.Field.TotalRecordCount} ,{ TableName.Rents.InternalValue}.*");
		sqlSelectFactory.FromQuery.Append($" FROM { TableName.Rents.InternalValue} ");
		sqlSelectFactory.WhereQuery.Append(" WHERE 1 = 1  ");
		sqlSelectFactory.OrderByQuery.Append($" ORDER BY { TableName.Rents.InternalValue}.{ Rents.Field.RentId}");
		sqlSelectFactory.OrderByQuery.Append($" { rentsSearchDetail.SortType }");
		sqlSelectFactory.OrderByQuery.Append($" OFFSET { rentsSearchDetail.StartFrom}  ROWS FETCH NEXT { rentsSearchDetail.Range} ROWS ONLY OPTION(RECOMPILE)");

		return this.GetCriteriaResult(sqlSelectFactory);
	}
}
