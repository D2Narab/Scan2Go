using DataLayer.Base.SqlGenerator;
using Scan2Go.Entity.Users;
using Scan2Go.Enums;

namespace Scan2Go.DataLayer.UsersDataLayer;

internal class UsersSql
{
    public static SqlSelectFactory GetUsersListItems()
    {
        SqlSelectFactory sqlSelectFactory = new SqlSelectFactory();

        sqlSelectFactory.SelectQuery.Append($"SELECT {TableName.Users.InternalValue}.{Users.Field.UserId},");
        sqlSelectFactory.SelectQuery.Append($"{TableName.Users.InternalValue}.{Users.Field.UserName},{TableName.Users.InternalValue}.{Users.Field.UserSurname},");
        sqlSelectFactory.SelectQuery.Append($"{TableName.Users.InternalValue}.{Users.Field.UserCode},{TableName.Users.InternalValue}.{Users.Field.IsActive} ");

        sqlSelectFactory.FromQuery.AppendLine($"FROM {TableName.Users.InternalValue}  ");

        return sqlSelectFactory;
    }
}