using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scan2Go.Api.BaseClasses;
using Scan2Go.Mapper.Managers;
using Utility.Core;

namespace Scan2Go.Api.Controllers;

[Route("[controller]")]
public class MonitoringController : BaseController
{
    [AllowAnonymous]
    [HttpGet]
    [Route("GetMails")]
    public async Task<IActionResult> GetMails()
    {
        OperationResult operationResult = await new MonitoringManager(this.CurrentUser).GetMails();
        return this.ReturnOperationResult(operationResult);
    }
}