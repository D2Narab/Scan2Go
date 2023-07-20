using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scan2Go.Api.BaseClasses;
using Scan2Go.Api.Services;
using Scan2Go.Mapper.Managers;
using Utility.Core;

namespace Scan2Go.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : BaseController
    {
        private readonly ILoginService loginService;

        public UsersController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetUsersList")]
        public IActionResult GetUsersList()
        {
            OperationResult operationResult = new UsersManager(this.CurrentUser).GetUsersList();
            return this.ReturnOperationResult(operationResult);
        }
    }
}
