using Scan2Go.BusinessLogic.BaseClasses;
using Scan2Go.DataLayer.UsersDataLayer;
using Scan2Go.Entity.Users;
using Scan2Go.Enums.Properties;
using Scan2Go.Enums;
using Scan2Go.Facade;
using System.Data;
using Utility.Bases;
using Utility.Bases.EntityBases;
using Utility.Core;
using Utility.Enum;
using Utility.Extensions;

namespace Scan2Go.BusinessLogic.UsersBusinessLogic;

public class UsersBusiness : BaseBusiness
{
    private UsersFacade _usersFacade;
    private UsersLogic _usersLogic;
    private UsersValidation _usersValidation;

    public UsersBusiness(OperationResult operationResult, IUser currentUser) : base(operationResult, currentUser)
    {
    }

    public UsersBusiness(BaseBusiness baseBusiness) : base(baseBusiness)
    {
    }

    private UsersFacade UsersFacade => _usersFacade ??= new UsersFacade(this.Language);

    private UsersLogic UsersLogic => _usersLogic ??= new UsersLogic(this);
    private UsersValidation UsersValidation => _usersValidation ??= new UsersValidation(this);

    public Users GetUser(int userId)
    {
        return UsersFacade.GetUser(userId);
    }

    public Users GetUsers(int userId)
    {
        Users users = UsersFacade.GetUsers(userId);

        return users;
    }

    public ListSourceBase GetUsersList()
    {
        return UsersFacade.GetUsersListItems();
    }

    public Users Login(string userName, string password, bool needsValidationFromContext = true)
    {
        UsersValidation.Login(userName, password);

        Users users = null;

        if (this.OperationState)
        {
            users = UsersFacade.Login(userName, password);
        }

        UsersValidation.Login(userName, password);

        if (this.OperationState)
        {
            //UsersLogic.Login(users);
        }
        else
        {
            users = null;
        }

        return users;
    }

    
}