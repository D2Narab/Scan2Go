using BusinessLogic.CustomersBusinessLogic;
using Scan2Go.BusinessLogic.BaseClasses;
using Scan2Go.Entity.Customers;
using Facade.CustomersFacade;
using Utility.Bases;
using Utility.Bases.EntityBases;
using Utility.Core;

namespace Scan2Go.BusinessLogic.CustomersBusinessLogic;
public class CustomersBusiness : BaseBusiness 
{
	private CustomersFacade _customersFacade;
	private CustomersLogic _customersLogic;
	private CustomersValidation _customersValidation;

	public CustomersBusiness(OperationResult operationResult, IUser currentUser) : base(operationResult, currentUser)
	{
	}

	public CustomersBusiness(BaseBusiness baseBusiness) : base(baseBusiness)
	{
	}

	private CustomersFacade CustomersFacade => _customersFacade ??= new CustomersFacade(Language);
	private CustomersLogic CustomersLogic => _customersLogic ??= new CustomersLogic(this);
	private CustomersValidation CustomersValidation => _customersValidation ??= new CustomersValidation(this);

	public void DeleteCustomers(int customersId)
	{
		Customers customers = CustomersFacade.GetCustomers(customersId);

		CustomersValidation.DeleteCustomers(customers);

		if (this.OperationState)
		{
			this.AddDetailResult(CustomersFacade.DeleteCustomers(customers));
		}
	}

	public Customers GetCustomers(int id)
	{
		return CustomersFacade.GetCustomers(id);
	}

	public ListSourceBase GetCustomersForList(CustomersSearchCriteria customersSearchCriteria)
	{
		ListSourceBase listSourceBase = CustomersFacade.GetCustomersForList(customersSearchCriteria);
		return listSourceBase;
	}

	public void SaveCustomers(Customers customers)
	{
		CustomersValidation.SaveCustomers(customers);

		if (this.OperationState)
		{
			this.AddDetailResult(CustomersFacade.SaveCustomers(customers));
		}
	}
}
