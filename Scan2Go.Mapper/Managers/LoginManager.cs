using AutoMapper;
using Scan2Go.BusinessLogic.UsersBusinessLogic;
using Scan2Go.Entity.Users;
using Scan2Go.Mapper.Models.UserModels;
using Utility.Core;

namespace Scan2Go.Mapper.Managers;

public class LoginManager
{
    public LoginManager()
    {
    }

    public OperationResult LoginAsync(string userName, string password, bool needsValidationFromContext = true)
    {
        OperationResult operationResult = new OperationResult();

        Users users = new UsersBusiness(operationResult, new UsersModel()).Login(userName, password, needsValidationFromContext);

        var config = new MapperConfiguration(cfg =>
        {
            //cfg.CreateMap<Definition, DefinitionModel>();
            //cfg.CreateMap<DefDetailSchema, DefDetailSchemaModel>();

            cfg.CreateMap<Users, UsersModel>();
        });

        var mapper = config.CreateMapper();

        UsersModel usersModels = mapper.Map<Users, UsersModel>(users);
        operationResult.ResultObject = usersModels;
        return operationResult;
    }
}