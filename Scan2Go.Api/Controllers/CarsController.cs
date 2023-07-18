using Microsoft.AspNetCore.Authorization;
using Scan2Go.Api.BaseClasses;
using Scan2Go.Mapper.CarsMappings;
using Scan2Go.Mapper.Models.CarsModels;
using Microsoft.AspNetCore.Mvc;
using Utility.Core;

namespace Scan2Go.Service.Controllers;

[Route("[controller]")]
public class CarsController : BaseController
{
	[HttpGet]
	[Route("DeleteCars/{carsId:int}")]
	public IActionResult DeleteCars(int carsId)
	{
		OperationResult operationResult = new CarsManager(this.CurrentUser).DeleteCars(carsId);
		return this.ReturnOperationResult(operationResult);
	}

	[HttpGet]
	[Route("GetCars/{carsId:int}")]
	public IActionResult GetCars(int carsId)
	{
		OperationResult operationResult = new CarsManager(this.CurrentUser).GetCars(carsId);
		return this.ReturnOperationResult(operationResult);
	}

	[AllowAnonymous]
	[HttpPost]
	[Route("GetCarsList")]
	public IActionResult GetCarsList(CarsSearchCriteriaModel carsSearchCriteriaModel)
	{
		OperationResult operationResult = new CarsManager(this.CurrentUser).GetCarsForList(carsSearchCriteriaModel);
		return this.ReturnOperationResult(operationResult);
	}

	[HttpPost]
	[Route("SaveCars")]
	public IActionResult SaveCars(CarsModel carsModel)
	{
		OperationResult operationResult = new CarsManager(this.CurrentUser).SaveCars(carsModel);
		return this.ReturnOperationResult(operationResult);
	}
}
