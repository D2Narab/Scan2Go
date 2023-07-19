using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scan2Go.Api.BaseClasses;
using Scan2Go.Api.Filters;
using Scan2Go.Api.Services;
using Scan2Go.Mapper.Models.UserModels;

namespace Scan2Go.Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public class LoginController : BaseController
{
    private readonly AppSettings _appSettings;

    private ILoginService _userService;

    public LoginController(ILoginService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    [HttpPost("Authenticate")]
    [AllowAnonymous]
    public IActionResult Authenticate(UsersModel model)
    {
        var response = _userService.Authenticate(model);

        if (response == null)
        {
            return Unauthorized(new { message = "Username or password is incorrect" });
        }

        return Ok(response);
    }
}