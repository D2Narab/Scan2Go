using BusinessLogic.CarsBusinessLogic;
using Scan2Go.BusinessLogic.BaseClasses;
using Scan2Go.Entity.Cars;
using Facade.CarsFacade;
using Utility.Bases;
using Utility.Bases.EntityBases;
using Utility.Core;

namespace Scan2Go.BusinessLogic.CarsBusinessLogic;
public class CarsBusiness : BaseBusiness 
{
	private CarsFacade _carsFacade;
	private CarsLogic _carsLogic;
	private CarsValidation _carsValidation;

	public CarsBusiness(OperationResult operationResult, IUser currentUser) : base(operationResult, currentUser)
	{
	}

	public CarsBusiness(BaseBusiness baseBusiness) : base(baseBusiness)
	{
	}

	private CarsFacade CarsFacade => _carsFacade ??= new CarsFacade(Language);
	private CarsLogic CarsLogic => _carsLogic ??= new CarsLogic(this);
	private CarsValidation CarsValidation => _carsValidation ??= new CarsValidation(this);

	public void DeleteCars(int carsId)
	{
		Cars cars = CarsFacade.GetCars(carsId);

		CarsValidation.DeleteCars(cars);

		if (this.OperationState)
		{
			this.AddDetailResult(CarsFacade.DeleteCars(cars));
		}
	}

	public Cars GetCars(int id)
	{
		return CarsFacade.GetCars(id);
	}

	public ListSourceBase GetCarsForList(CarsSearchCriteria carsSearchCriteria)
	{
		ListSourceBase listSourceBase = CarsFacade.GetCarsForList(carsSearchCriteria);
		return listSourceBase;
	}

	public void SaveCars(Cars cars)
	{
		CarsValidation.SaveCars(cars);

		if (this.OperationState)
		{
			this.AddDetailResult(CarsFacade.SaveCars(cars));
		}
	}
}
