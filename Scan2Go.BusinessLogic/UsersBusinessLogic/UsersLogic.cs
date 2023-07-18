using Scan2Go.BusinessLogic.BaseClasses;

namespace Scan2Go.BusinessLogic.UsersBusinessLogic;

public class UsersLogic
{
    private readonly BaseBusiness baseBusiness;

    public UsersLogic(BaseBusiness baseBusiness)
    {
        this.baseBusiness = baseBusiness;
    }
}