using DataLayer.Base.SqlGenerator;
using Scan2Go.DataLayer.BaseClasses.DataLayersBases;
using Scan2Go.Entity.Definitions;
using Scan2Go.Enums.Properties;
using System.Data;
using System.Data.SqlClient;
using Utility.Core;
using Utility.Core.DataLayer;

namespace Scan2Go.DataLayer.DefinitionDataLayer;

public class DefinitionDAO : Scan2GoDataLayerBase
{
    //public DataTable GetDefinitionList(string defName)
    //{
    //    SqlSelectFactory sqlSelectFactory = DefinitionSql.GetDefinitionList(defName);

    //    return this.ExecuteSQLDataTable(sqlSelectFactory);
    //}

    public OperationResult DeleteDefinition(Definition definition)
    {
        OperationResult operationResult = new OperationResult();
        try
        {
            this.BeginTransaction();

            new DefinitionDMLOperations(this).DeleteDefinition(definition);

            operationResult.State = this.CommitTransaction();
        }
        catch (Exception exception)
        {
            SqlException sqlException = exception as SqlException;

            if (sqlException != null && sqlException.Number == 547)
            {
                operationResult.MessageStringKey = nameof(MessageStrings.DefinitionIsUsedAnotherRecord);
            }
            else
            {
                operationResult.Exception = exception;
            }

            operationResult.State = false;

            this.RollbackTransaction();
        }

        return operationResult;
    }

    public DataRow GetMainPropertiesOfDefinition(string tableName)
    {
        SqlSelectFactory sqlSelectFactory = DefinitionSql.GetMainPropertiesOfDefinition();

        sqlSelectFactory.AddParam(new DatabaseParameter
        {
            FieldName = Definition.Field.TableName,
            DbType = DbType.String,
            ParameterDirection = ParameterDirection.Input,
            FieldValue = tableName
        });

        DataRow dataRow = this.ExecuteSQLDataRow(sqlSelectFactory);

        return dataRow;
    }

    public OperationResult SaveDefinition(Definition definition)
    {
        OperationResult operationResult = new OperationResult();

        try
        {
            this.BeginTransaction();

            if (definition.defDetailSchemas.Any())
            {
                new DefinitionDMLOperations(this).SaveDefinition(definition);
            }

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