using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scan2Go.Api.BaseClasses;
using Scan2Go.Mapper.CustomersMappings;
using Scan2Go.Mapper.Models.CustomersModels;
using Utility.Core;

namespace Scan2Go.Service.Controllers;

[Route("[controller]")]
public class CustomersController : BaseController
{
    [AllowAnonymous]
    [HttpPost]
    [Route("CreateCustomer")]
    public IActionResult CreateCustomer(CustomersModel customersModel)
    {
        OperationResult operationResult = new CustomersManager(this.CurrentUser).CreateCustomer(customersModel);
        return this.ReturnOperationResult(operationResult);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("DeleteCustomers/{customersId:int}")]
    public IActionResult DeleteCustomers(int customersId)
    {
        OperationResult operationResult = new CustomersManager(this.CurrentUser).DeleteCustomers(customersId);
        return this.ReturnOperationResult(operationResult);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("GetCustomers/{customersId:int}")]
    public IActionResult GetCustomers(int customersId)
    {
        OperationResult operationResult = new CustomersManager(this.CurrentUser).GetCustomers(customersId);
        return this.ReturnOperationResult(operationResult);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("GetCustomersList")]
    public IActionResult GetCustomersList(CustomersSearchCriteriaModel customersSearchCriteriaModel)
    {
        OperationResult operationResult = new CustomersManager(this.CurrentUser).GetCustomersForList(customersSearchCriteriaModel);
        return this.ReturnOperationResult(operationResult);
    }

    [AllowAnonymous]
    [HttpPatch]
    [Route("SaveCustomers")]
    public IActionResult SaveCustomers(CustomersModel customersModel)
    {
        OperationResult operationResult = new CustomersManager(this.CurrentUser).SaveCustomers(customersModel);
        return this.ReturnOperationResult(operationResult);
    }
}