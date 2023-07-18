﻿using Scan2Go.BusinessLogic.BaseClasses;
using Scan2Go.Entity.Users;
using Scan2Go.Facade;
using Utility.Bases;
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

    public Users GetUser(int userId)
    {
        return UsersFacade.GetUser(userId);
    }
}