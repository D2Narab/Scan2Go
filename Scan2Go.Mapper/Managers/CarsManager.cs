using Scan2Go.BusinessLogic.CarsBusinessLogic;
using Scan2Go.Entity.Cars;
using Scan2Go.Enums;
using Scan2Go.Mapper.BaseClasses;
using Scan2Go.Mapper.Models.CarsModels;
using Utility.Bases;
using Utility.Bases.EntityBases;
using Utility.Core;

namespace Scan2Go.Mapper.CarsMappings;

public class CarsManager : BaseManager
{
	public CarsManager(IUser user) : base(user)
	{
	}

	public OperationResult DeleteCars(int carsId)
	{
		OperationResult operationResult = new OperationResult((int)Modules.Cars, (int)Operations.DeleteCars);

		 new CarsBusiness(operationResult, this.user).DeleteCars(carsId);

		return operationResult;
	}

	public OperationResult GetCars(int carsId)
	{
		OperationResult operationResult = new OperationResult();

		Cars cars  = new CarsBusiness(operationResult, this.user).GetCars(carsId);

		CarsModel carsModel = Mapper.Map<Cars,CarsModel>(cars);

		operationResult.ResultObject = carsModel;
		return operationResult;
	}

	public OperationResult GetCarsForList(CarsSearchCriteriaModel carsSearchCriteriaModel)
	{
		OperationResult operationResult = new OperationResult();

		CarsSearchCriteria carsSearchCriteria = Mapper.Map<CarsSearchCriteriaModel, CarsSearchCriteria>(carsSearchCriteriaModel);

		ListSourceBase carsListItems = new CarsBusiness(operationResult, this.user).GetCarsForList(carsSearchCriteria);

		operationResult.ResultObject = Mapper.Map<ListSourceBase, ListSourceModel<CarsListItemModel>>(carsListItems);
		return operationResult;
	}

	public OperationResult SaveCars(CarsModel carsModel)
	{
		var cars = Mapper.Map<CarsModel,Cars>(carsModel);

		OperationResult operationResult = new OperationResult((int)Modules.Cars, (int)Operations.SaveCars);

		new CarsBusiness(operationResult, this.user).SaveCars(cars);

		operationResult.ResultObject = cars;
		return operationResult;
	}
}
