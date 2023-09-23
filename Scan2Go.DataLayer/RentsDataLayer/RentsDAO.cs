using DataLayer.Base.SqlGenerator;
using Scan2Go.DataLayer.BaseClasses.DataLayersBases;
using Scan2Go.DataLayer.CarsDataLayer;
using Scan2Go.Entity.Rents;
using System;
using System.Data;
using Utility.Bases.EntityBases;
using Utility.Core;
using Utility.Core.DataLayer;
using Utility.Extensions;

namespace Scan2Go.DataLayer.RentsDataLayer;
public class RentsDAO : Scan2GoDataLayerBase
{
    public OperationResult DeleteRents(Rents rents)
    {
        OperationResult operationResult = new OperationResult();

        try
        {
            BeginTransaction();

            new RentsDMLOperations(this).DeleteRents(rents);

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

    public CriteriaResult GetRentsForList(RentsSearchCriteria rentsSearchCriteria)
    {
        RentsSearchDetail rentsSearchDetail = new RentsSearchDetail
        {
            RentsSearchCriteria = rentsSearchCriteria,
            OrderByColumn = rentsSearchCriteria.OrderByColumn,
            Range = rentsSearchCriteria.Range,
            SortType = rentsSearchCriteria.SortType,
            StartFrom = rentsSearchCriteria.StartFrom
        };

        return new RentsSelectOperations(this).GetRentsSearchDetailList(rentsSearchDetail);
    }

    public OperationResult SaveRents(Rents rents)
    {
        OperationResult operationResult = new OperationResult();

        try
        {
            this.BeginTransaction();

            new RentsDMLOperations(this).SaveRents(rents);

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

    public DataRow GetRentByCustomerName(string customerFullName)
    {
        SqlSelectFactory sqlSelectFactory = RentsSql.GetRentByCustomerName();

        sqlSelectFactory.AddParam(new DatabaseParameter
        {
            FieldName = "@FullName",
            DbType = DbType.String,
            ParameterDirection = ParameterDirection.Input,
            FieldValue = customerFullName
        });

        return this.ExecuteSQLDataRow(sqlSelectFactory);
    }
}
