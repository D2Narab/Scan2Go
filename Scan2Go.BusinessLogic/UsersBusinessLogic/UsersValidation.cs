using Scan2Go.BusinessLogic.BaseClasses;
using Scan2Go.Entity.Users;
using Scan2Go.Enums.Properties;
using Utility.Bases.EntityBases.UserBases;
using Utility.Core;

namespace Scan2Go.BusinessLogic.UsersBusinessLogic;

internal class UsersValidation
{
    private readonly BaseBusiness _baseBusiness;

    public UsersValidation(BaseBusiness baseBusiness)
    {
        this._baseBusiness = baseBusiness;
    }

    public void Login(string userName, string password)
    {
        if (string.IsNullOrEmpty(userName))
        {
            this._baseBusiness.AddDetailResult(new OperationResult { State = false, MessageStringKey = nameof(MessageStrings.UserNameCannotBeEmpty) });
        }

        if (string.IsNullOrEmpty(password))
        {
            this._baseBusiness.AddDetailResult(new OperationResult { State = false, MessageStringKey = nameof(MessageStrings.UserPasswordCannotBeEmpty) });
        }
    }

    public void CheckUser(Users users)
    {
        if (users.UserName.Equals("James") && users.UserSurname.Equals("Bond"))
        {
            this._baseBusiness.AddDetailResult(new OperationResult { State = false, MessageStringKey = nameof(MessageStrings.BondJamesBond) });
        }
    }
}