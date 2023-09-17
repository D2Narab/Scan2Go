using Scan2Go.BusinessLogic.UsersBusinessLogic;
using Scan2Go.Entity.Users;
using Scan2Go.Enums;
using Scan2Go.Mapper.BaseClasses;
using Scan2Go.Mapper.Models.UserModels;
using Utility.Bases;
using Utility.Bases.EntityBases;
using Utility.Core;
using Utility.Extensions;

namespace Scan2Go.Mapper.Managers;

public class UsersManager : BaseManager
{
    public UsersManager(IUser user) : base(user)
    {
    }

    public OperationResult CreateUser(UsersModel usersModel)
    {
        Users users = Mapper.Map<UsersModel, Users>(usersModel);

        OperationResult operationResult = new OperationResult(Modules.User.AsInt(), Operations.CreateUser.AsInt());

        new UsersBusiness(operationResult, this.user).SaveUsers(users);
        usersModel.UserId = users.UserId;

        return operationResult;
    }

    public OperationResult DeleteUser(int userId)
    {
        OperationResult operationResult = new OperationResult((int)Modules.User, (int)Operations.DeleteUser);

        new UsersBusiness(operationResult, this.user).DeleteUser(userId);

        return operationResult;
    }

    public OperationResult GetUsers(int userId)
    {
        OperationResult operationResult = new OperationResult();

        Users users = new UsersBusiness(operationResult, this.user).GetUsers(userId);
        UsersModel usersModels = Mapper.Map<Users, UsersModel>(users);
        operationResult.ResultObject = usersModels;

        return operationResult;
    }

    public OperationResult GetUsersList(UsersSearchCriteriaModel usersSearchCriteriaModel)
    {
        UsersSearchCriteria usersSearchCriteria = Mapper.Map<UsersSearchCriteriaModel, UsersSearchCriteria>(usersSearchCriteriaModel);

        OperationResult operationResult = new OperationResult();

        ListSourceBase users = new UsersBusiness(operationResult, this.user).GetUsersList(usersSearchCriteria);

        operationResult.ResultObject = Mapper.Map<ListSourceBase, ListSourceModel<UserListItemModel>>(users);

        return operationResult;
    }

    public OperationResult SaveUser(UsersModel usersModel)
    {
        Users users = Mapper.Map<UsersModel, Users>(usersModel);

        OperationResult operationResult = new OperationResult(Modules.User.AsInt(), Operations.SaveUser.AsInt());

        new UsersBusiness(operationResult, this.user).SaveUsers(users);
        usersModel.UserId = users.UserId;

        return operationResult;
    }
}