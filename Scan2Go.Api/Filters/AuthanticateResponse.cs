using Scan2Go.Mapper.Models.UserModels;

namespace Scan2Go.Api.Filters;

public class AuthenticateResponse
{
    public AuthenticateResponse(UsersModel user, string token)
    {
        dbUser = user;
        UserId = user.UserId;
        UserName = user.UserName;
        UserSurname = user.UserSurname;
        UserCode = user.UserCode;
        Token = token;
    }

    public UsersModel dbUser { get; set; }
    public string Token { get; set; }
    public string UserCode { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string UserSurname { get; set; }
}