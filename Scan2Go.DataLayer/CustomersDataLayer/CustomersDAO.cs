using Scan2Go.DataLayer.BaseClasses.DataLayersBases;
using Scan2Go.Entity.Customers;
using System;
using Utility.Bases.EntityBases;
using Utility.Core;

namespace Scan2Go.DataLayer.CustomersDataLayer;
public class CustomersDAO : Scan2GoDataLayerBase
{
	public OperationResult DeleteCustomers(Customers customers)
	{
		OperationResult operationResult = new OperationResult();

		try
		{
			BeginTransaction();

			new CustomersDMLOperations(this).DeleteCustomers(customers);

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

	public CriteriaResult GetCustomersForList(CustomersSearchCriteria customersSearchCriteria)
	{
		CustomersSearchDetail customersSearchDetail = new CustomersSearchDetail
		{
			CustomersSearchCriteria = customersSearchCriteria,
			OrderByColumn = customersSearchCriteria.OrderByColumn,
			Range = customersSearchCriteria.Range,
			SortType = customersSearchCriteria.SortType,
			StartFrom = customersSearchCriteria.StartFrom
		};

		return new CustomersSelectOperations(this).GetCustomersSearchDetailList(customersSearchDetail);
	}

	public OperationResult SaveCustomers(Customers customers)
	{
		OperationResult operationResult = new OperationResult();

		try
		{
			this.BeginTransaction();

			new CustomersDMLOperations(this).SaveCustomers(customers);

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
