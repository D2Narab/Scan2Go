using Scan2Go.Api.Filters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Scan2Go.Enums;
using Scan2Go.Mapper.Managers;
using Scan2Go.Mapper.Models.UserModels;
using Utility.Core;
using Microsoft.AspNetCore.Identity;
using Scan2Go.Api.Filters;
using Scan2Go.Mapper.Models.UserModels;

namespace Scan2Go.Api.Services;

public interface ILoginService
{
    AuthenticateResponse Authenticate(UsersModel model, bool needsValidationFromContext = true);

    UsersModel GetById(int id, int divisionId = 0);
}

public class LoginService : ILoginService
{
    private readonly AppSettings _appSettings;

    public LoginService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public AuthenticateResponse Authenticate(UsersModel model, bool needsValidationFromContext = true)
    {
        OperationResult operationResult = new LoginManager().LoginAsync(model.UserCode, model.Password, needsValidationFromContext);
        UsersModel user = operationResult.ResultObject;

        // return null if user not found
        if (user == null)
        {
            return null;
        }

        // authentication successful so generate jwt token
        var token = GenerateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    public UsersModel GetById(int id, int divisionId)
    {
        return new UsersManager(new UsersModel()).GetUsers(id).ResultObject;
    }

    private string GenerateJwtToken(UsersModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("qMCdFDQuF23RV1Y-1Gq9L3cF3VmuFwVbam4fMTdAfpo");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim(type:"UserId", user.UserId.ToString()),
                new Claim("UserCode",user.UserCode.ToString()),
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}