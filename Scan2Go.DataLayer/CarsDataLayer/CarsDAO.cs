using Scan2Go.DataLayer.BaseClasses.DataLayersBases;
using Scan2Go.Entity.Cars;
using System;
using Utility.Bases.EntityBases;
using Utility.Core;

namespace Scan2Go.DataLayer.CarsDataLayer;
public class CarsDAO : Scan2GoDataLayerBase
{
	public OperationResult DeleteCars(Cars cars)
	{
		OperationResult operationResult = new OperationResult();

		try
		{
			BeginTransaction();

			new CarsDMLOperations(this).DeleteCars(cars);

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

	public CriteriaResult GetCarsForList(CarsSearchCriteria carsSearchCriteria)
	{
		CarsSearchDetail carsSearchDetail = new CarsSearchDetail
		{
			CarsSearchCriteria = carsSearchCriteria,
			OrderByColumn = carsSearchCriteria.OrderByColumn,
			Range = carsSearchCriteria.Range,
			SortType = carsSearchCriteria.SortType,
			StartFrom = carsSearchCriteria.StartFrom
		};

		return new CarsSelectOperations(this).GetCarsSearchDetailList(carsSearchDetail);
	}

	public OperationResult SaveCars(Cars cars)
	{
		OperationResult operationResult = new OperationResult();

		try
		{
			this.BeginTransaction();

			new CarsDMLOperations(this).SaveCars(cars);

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
