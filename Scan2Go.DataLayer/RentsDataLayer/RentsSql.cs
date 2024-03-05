using Scan2Go.Entity.Rents;
using DataLayer.Base.SqlGenerator;
using Scan2Go.Entity.Customers;
using Scan2Go.Enums;

namespace Scan2Go.DataLayer.RentsDataLayer;
public class RentsSql
{
	public static SqlInsertFactory SaveRents(Rents rents)
	{
		SqlInsertFactory mainFactory = new SqlInsertFactory(rents);
		return mainFactory;
	}

    internal static SqlSelectFactory GetRentByCustomerName()
    {
        SqlSelectFactory sqlSelectFactory = new SqlSelectFactory();

        sqlSelectFactory.SelectQuery.Append("SELECT * ");
        sqlSelectFactory.FromQuery.AppendLine($"FROM {TableName.Rents.InternalValue} ");
        sqlSelectFactory.FromQuery.AppendLine($"INNER JOIN {TableName.Customers.InternalValue} ON {TableName.Customers.InternalValue}.{Customers.Field.CustomerId} = ");
        sqlSelectFactory.FromQuery.AppendLine($"{TableName.Rents.InternalValue}.{Customers.Field.CustomerId} ");
        sqlSelectFactory.WhereQuery.AppendLine($"WHERE {TableName.Customers.InternalValue}.{Customers.Field.CustomerName}");
        sqlSelectFactory.WhereQuery.AppendLine($"+' '+{TableName.Customers.InternalValue}.{Customers.Field.CustomerSurname} ");
        sqlSelectFactory.WhereQuery.AppendLine($"= @FullName");

        return sqlSelectFactory;
    }

    public static SqlSelectFactory GetRentByPassportNumber()
    {
        SqlSelectFactory sqlSelectFactory = new SqlSelectFactory();

        sqlSelectFactory.SelectQuery.Append("SELECT * ");
        sqlSelectFactory.FromQuery.AppendLine($"FROM {TableName.Rents.InternalValue} ");
        sqlSelectFactory.FromQuery.AppendLine($"INNER JOIN {TableName.Customers.InternalValue} ON {TableName.Customers.InternalValue}.{Customers.Field.CustomerId} = ");
        sqlSelectFactory.FromQuery.AppendLine($"{TableName.Rents.InternalValue}.{Customers.Field.CustomerId} ");
        sqlSelectFactory.WhereQuery.AppendLine($"WHERE {TableName.Customers.InternalValue}.{Customers.Field.PassportNumber}");
        sqlSelectFactory.WhereQuery.AppendLine("= @PassportNumber");

        return sqlSelectFactory;
    }
}
