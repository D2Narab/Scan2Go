using Scan2Go.DataLayer.BaseClasses.SelectOperationBases;
using Scan2Go.Entity.Customers;
using Scan2Go.Enums;
using DataLayer.Base.DatabaseFactory;
using DataLayer.Base.SqlGenerator;
using Utility.Bases.EntityBases;

namespace Scan2Go.DataLayer.CustomersDataLayer;
public class CustomersSelectOperations : Scan2GoSelectOperations
{
	public CustomersSelectOperations(DataLayerBase dataLayerBase) : base(dataLayerBase)
	{
	}

	public CriteriaResult GetCustomersSearchDetailList(CustomersSearchDetail customersSearchDetail)
	{
		SqlSelectFactory sqlSelectFactory = new SqlSelectFactory();

		sqlSelectFactory.SelectQuery.Append($" SELECT COUNT(*) OVER() AS { CriteriaResult.Field.TotalRecordCount} ,{ TableName.Customers.InternalValue}.*");
		sqlSelectFactory.FromQuery.Append($" FROM { TableName.Customers.InternalValue} ");
		sqlSelectFactory.WhereQuery.Append(" WHERE 1 = 1  ");
		sqlSelectFactory.OrderByQuery.Append($" ORDER BY { TableName.Customers.InternalValue}.{ Customers.Field.CustomerId}");
		sqlSelectFactory.OrderByQuery.Append($" { customersSearchDetail.SortType }");
		sqlSelectFactory.OrderByQuery.Append($" OFFSET { customersSearchDetail.StartFrom}  ROWS FETCH NEXT { customersSearchDetail.Range} ROWS ONLY OPTION(RECOMPILE)");

		return this.GetCriteriaResult(sqlSelectFactory);
	}
}
