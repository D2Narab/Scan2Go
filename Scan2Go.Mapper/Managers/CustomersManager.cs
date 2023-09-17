using Scan2Go.BusinessLogic.CustomersBusinessLogic;
using Scan2Go.Entity.Customers;
using Scan2Go.Enums;
using Scan2Go.Mapper.BaseClasses;
using Scan2Go.Mapper.Models.CustomersModels;
using Utility.Bases;
using Utility.Bases.EntityBases;
using Utility.Core;

namespace Scan2Go.Mapper.CustomersMappings;

public class CustomersManager : BaseManager
{
	public CustomersManager(IUser user) : base(user)
	{
	}

	public OperationResult DeleteCustomers(int customersId)
	{
		OperationResult operationResult = new OperationResult((int)Modules.Customers, (int)Operations.DeleteCustomers);

		 new CustomersBusiness(operationResult, this.user).DeleteCustomers(customersId);

		return operationResult;
	}

	public OperationResult GetCustomers(int customersId)
	{
		OperationResult operationResult = new OperationResult();

		Customers customers  = new CustomersBusiness(operationResult, this.user).GetCustomers(customersId);

		CustomersModel customersModel = Mapper.Map<Customers,CustomersModel>(customers);

		operationResult.ResultObject = customersModel;
		return operationResult;
	}

	public OperationResult GetCustomersForList(CustomersSearchCriteriaModel customersSearchCriteriaModel)
	{
		OperationResult operationResult = new OperationResult();

		CustomersSearchCriteria customersSearchCriteria = Mapper.Map<CustomersSearchCriteriaModel, CustomersSearchCriteria>(customersSearchCriteriaModel);

		ListSourceBase customersListItems = new CustomersBusiness(operationResult, this.user).GetCustomersForList(customersSearchCriteria);

		operationResult.ResultObject = Mapper.Map<ListSourceBase, ListSourceModel<CustomersListItemModel>>(customersListItems);
		return operationResult;
	}

	public OperationResult SaveCustomers(CustomersModel customersModel)
	{
		var customers = Mapper.Map<CustomersModel,Customers>(customersModel);

		OperationResult operationResult = new OperationResult((int)Modules.Customers, (int)Operations.SaveCustomers);

		new CustomersBusiness(operationResult, this.user).SaveCustomers(customers);

		operationResult.ResultObject = customers;
		return operationResult;
	}
}
