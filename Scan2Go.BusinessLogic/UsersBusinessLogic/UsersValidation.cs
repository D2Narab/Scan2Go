using Scan2Go.BusinessLogic.BaseClasses;

namespace Scan2Go.BusinessLogic.UsersBusinessLogic;

internal class UsersValidation
{
    private readonly BaseBusiness baseBusiness;

    public UsersValidation(BaseBusiness baseBusiness)
    {
        this.baseBusiness = baseBusiness;
    }
}