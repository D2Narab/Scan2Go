using Scan2Go.BusinessLogic.BaseClasses;

namespace BusinessLogic.CustomersBusinessLogic;

public class CustomersLogic
{
	private readonly BaseBusiness _baseBusiness;

	public CustomersLogic(BaseBusiness baseBusiness)
	{
		_baseBusiness = baseBusiness;
	}
}
