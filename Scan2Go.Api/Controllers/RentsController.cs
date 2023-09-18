using Microsoft.AspNetCore.Authorization;
using Scan2Go.Api.BaseClasses;
using Scan2Go.Mapper.RentsMappings;
using Scan2Go.Mapper.Models.RentsModels;
using Microsoft.AspNetCore.Mvc;
using Utility.Core;

namespace Scan2Go.Service.Controllers;

[Route("[controller]")]
public class RentsController : BaseController
{
	[HttpGet]
	[Route("DeleteRents/{rentsId:int}")]
	public IActionResult DeleteRents(int rentsId)
	{
		OperationResult operationResult = new RentsManager(this.CurrentUser).DeleteRents(rentsId);
		return this.ReturnOperationResult(operationResult);
	}

	[HttpGet]
	[Route("GetRents/{rentsId:int}")]
	public IActionResult GetRents(int rentsId)
	{
		OperationResult operationResult = new RentsManager(this.CurrentUser).GetRents(rentsId);
		return this.ReturnOperationResult(operationResult);
	}

	[AllowAnonymous]
	[HttpPost]
	[Route("GetRentsList")]
	public IActionResult GetRentsList(RentsSearchCriteriaModel rentsSearchCriteriaModel)
	{
		OperationResult operationResult = new RentsManager(this.CurrentUser).GetRentsForList(rentsSearchCriteriaModel);
		return this.ReturnOperationResult(operationResult);
	}

	[HttpPost]
	[Route("SaveRents")]
	public IActionResult SaveRents(RentsModel rentsModel)
	{
		OperationResult operationResult = new RentsManager(this.CurrentUser).SaveRents(rentsModel);
		return this.ReturnOperationResult(operationResult);
	}
}
