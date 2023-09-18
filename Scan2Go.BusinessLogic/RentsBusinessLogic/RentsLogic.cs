using Scan2Go.BusinessLogic.BaseClasses;

namespace BusinessLogic.RentsBusinessLogic;

public class RentsLogic
{
	private readonly BaseBusiness _baseBusiness;

	public RentsLogic(BaseBusiness baseBusiness)
	{
		_baseBusiness = baseBusiness;
	}
}
