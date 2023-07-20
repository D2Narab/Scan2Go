﻿using DataLayer.Base.SqlGenerator;
using Scan2Go.DataLayer.BaseClasses.DataLayersBases;
using Scan2Go.DataLayer.BaseClasses.SelectOperationBases;
using Scan2Go.Entity.Users;
using Scan2Go.Enums;
using System.Data;
using Utility.Core.DataLayer;

namespace Scan2Go.DataLayer.UsersDataLayer;

public class UsersDAO : Scan2GoDataLayerBase
{
    public DataRow Login(string userName, string password)
    {
        List<DatabaseParameter> sqlparams = new List<DatabaseParameter>();

        string userCode = userName;

        sqlparams.Add(new DatabaseParameter { FieldName = "@UserCode", DbType = DbType.String, ParameterDirection = ParameterDirection.Input, FieldValue = userCode });
        sqlparams.Add(new DatabaseParameter { FieldName = "@Password", DbType = DbType.String, ParameterDirection = ParameterDirection.Input, FieldValue = password });
        sqlparams.Add(new DatabaseParameter { FieldName = "@IsActive", DbType = DbType.Boolean, ParameterDirection = ParameterDirection.Input, FieldValue = true });

        return new Scan2GoSelectOperations().GetEntityDataRow<Users>(sqlparams);
    }

    public DataTable GetUsersListItems()
    {
        SqlSelectFactory sqlSelectFactory = UsersSql.GetUsersListItems();
        
        return this.ExecuteSQLDataTable(sqlSelectFactory);
    }
}