using Scan2Go.BusinessLogic.BaseClasses;
using Scan2Go.Entity.Users;
using Scan2Go.Facade;
using Utility.Bases;
using Utility.Bases.EntityBases;
using Utility.Core;

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

    public void DeleteUser(int userId)
    {
        this.AddDetailResult(UsersFacade.DeleteUser(userId));
    }

    public Users GetUser(int userId)
    {
        return UsersFacade.GetUser(userId);
    }

    public Users GetUsers(int userId)
    {
        return UsersFacade.GetUsers(userId);
    }

    public ListSourceBase GetUsersList(UsersSearchCriteria usersSearchCriteria)
    {
        return UsersFacade.GetUsersListItems(usersSearchCriteria);
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

    public void SaveUsers(Users users)
    {
        UsersValidation.CheckUser(users);

        if (this.OperationState == false)
        {
            return;
        }

        this.AddDetailResult(UsersFacade.SaveUsers(users));
    }
}