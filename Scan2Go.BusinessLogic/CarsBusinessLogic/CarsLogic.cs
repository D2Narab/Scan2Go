using Scan2Go.BusinessLogic.BaseClasses;

namespace BusinessLogic.CarsBusinessLogic;

public class CarsLogic
{
	private readonly BaseBusiness _baseBusiness;

	public CarsLogic(BaseBusiness baseBusiness)
	{
		_baseBusiness = baseBusiness;
	}
}
