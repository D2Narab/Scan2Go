﻿using Scan2Go.BusinessLogic.UsersBusinessLogic;
using Scan2Go.Entity.Users;
using Scan2Go.Mapper.BaseClasses;
using Scan2Go.Mapper.Models.UserModels;
using Utility.Bases;
using Utility.Core;

namespace Scan2Go.Mapper.Managers;

public class UsersManager : BaseManager
{
    public UsersManager(IUser user) : base(user)
    {
    }

    public OperationResult GetUsers(int userId)
    {
        OperationResult operationResult = new OperationResult();

        UsersBusiness usersBusiness = new UsersBusiness(operationResult, this.user);

        Users users = usersBusiness.GetUsers(userId);
        UsersModel usersModels = Mapper.Map<Users, UsersModel>(users);
        operationResult.ResultObject = usersModels;

        return operationResult;
    }
}