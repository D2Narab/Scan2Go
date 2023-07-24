using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scan2Go.Api.BaseClasses;
using Scan2Go.Api.Services;
using Scan2Go.Mapper.Managers;
using Scan2Go.Mapper.Models.UserModels;
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
        [HttpDelete]
        [Route("DeleteUser/{userId:int}")]
        public IActionResult DeleteUser(int userId)
        {
            OperationResult operationResult = new UsersManager(this.CurrentUser).DeleteUser(userId);
            return this.ReturnOperationResult(operationResult);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetUser/{userId:int}")]
        public IActionResult GetUser(int userId)
        {
            OperationResult operationResult = new UsersManager(this.CurrentUser).GetUsers(userId);
            return this.ReturnOperationResult(operationResult);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetUsersList")]
        public IActionResult GetUsersList()
        {
            OperationResult operationResult = new UsersManager(this.CurrentUser).GetUsersList();
            return this.ReturnOperationResult(operationResult);
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("SaveUser")]
        public IActionResult SaveUser(UsersModel usersModel)
        {
            OperationResult operationResult = new UsersManager(this.CurrentUser).SaveUser(usersModel);
            return this.ReturnOperationResult(operationResult);
        }
    }
}