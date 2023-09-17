using DataLayer.Base.SqlGenerator;
using Scan2Go.DataLayer.BaseClasses.DataLayersBases;
using Scan2Go.DataLayer.BaseClasses.SelectOperationBases;
using Scan2Go.Entity.Users;
using System.Data;
using Utility.Core;
using Utility.Core.DataLayer;

namespace Scan2Go.DataLayer.UsersDataLayer;

public class UsersDAO : Scan2GoDataLayerBase
{
    public OperationResult DeleteUser(Users user)
    {
        OperationResult operationResult = new OperationResult();

        try
        {
            this.BeginTransaction();

            new UsersDMLOperations(this).DeleteUser(user);

            operationResult.State = this.CommitTransaction();
        }
        catch (Exception exception)
        {
            operationResult.State = false;
            operationResult.Exception = exception;
            this.RollbackTransaction();
        }

        return operationResult;
    }

    //TODO will use the Criteria later if needed.
    public DataTable GetUsersListItems(UsersSearchCriteria usersSearchCriteria)
    {
        SqlSelectFactory sqlSelectFactory = UsersSql.GetUsersListItems();

        return this.ExecuteSQLDataTable(sqlSelectFactory);
    }

    public DataRow Login(string userName, string password)
    {
        List<DatabaseParameter> sqlparams = new List<DatabaseParameter>();

        string userCode = userName;

        sqlparams.Add(new DatabaseParameter { FieldName = "@UserCode", DbType = DbType.String, ParameterDirection = ParameterDirection.Input, FieldValue = userCode });
        sqlparams.Add(new DatabaseParameter { FieldName = "@Password", DbType = DbType.String, ParameterDirection = ParameterDirection.Input, FieldValue = password });
        sqlparams.Add(new DatabaseParameter { FieldName = "@IsActive", DbType = DbType.Boolean, ParameterDirection = ParameterDirection.Input, FieldValue = true });

        return new Scan2GoSelectOperations().GetEntityDataRow<Users>(sqlparams);
    }

    public OperationResult SaveUsers(Users users)
    {
        OperationResult operationResult = new OperationResult();

        try
        {
            this.BeginTransaction();

            new UsersDMLOperations(this).SaveUsers(users);

            operationResult.State = this.CommitTransaction();
        }
        catch (Exception exception)
        {
            operationResult.State = false;
            operationResult.Exception = exception;
            this.RollbackTransaction();
        }

        return operationResult;
    }
}