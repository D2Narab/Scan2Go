using Scan2Go.Enums;
using Utility.Core;
using Utility.Core.LogClasses;
using Utility.Enum;

namespace Scan2Go.BusinessLogic.BaseClasses;

public class BaseBusiness
{
    private readonly OperationResult mainOperationResult;
    //private readonly IUser user;

    public BaseBusiness(OperationResult operationResult/*, IUser user*/)
    {
        this.mainOperationResult = operationResult;
        //this.user = user;
    }

    public BaseBusiness(BaseBusiness baseBusiness)
    {
        this.mainOperationResult = baseBusiness.mainOperationResult;
        //this.user = baseBusiness.user;
    }

    //public int CurrentUserDivisionId { get { return this.user.GetDivisionId(); } }

    //public int CurrentUserLaboratoryId { get { return this.user.GetLaboratoryId(); } }

    //public bool DoesCurrentUserHaveSuperUserRole
    //{
    //    get
    //    {
    //        return this.GetCurrentUser().DoesHaveSuperUserRole();
    //    }
    //}

    public LanguageEnum Language
    {
        get
        {
            //if (this.user == null)
            //{
            return LanguageEnum.EN;
            //}

            // return this.user.GetLanguage();
        }
    }

    public bool OperationState
    { get { return this.mainOperationResult.State; } }

    //public string UserCode { get { return this.user.GetUserCode(); } }

    //public string UserFullName { get { return this.user.GetFullName(); } }

    //public int UserId { get { return this.user.GetUserId(); } }

    //public string UserName { get { return this.user.GetName(); } }

    //public string UserSurname { get { return this.user.GetSurname(); } }
    public void AddDetailResult(OperationResult operationResult, List<MessageParameters> MessageParameters = null)
    {
        if (!string.IsNullOrEmpty(operationResult.MessageStringKey))
            operationResult.Message = EnumMethods.GetResourceString(operationResult.MessageStringKey, Language, MessageParameters);

        this.mainOperationResult.DetailResults.Add(operationResult);
    }

    public void AddDetailResult(OperationResult operationResult, MessageParameters MessageParameter)
    {
        if (!string.IsNullOrEmpty(operationResult.MessageStringKey))
            operationResult.Message = EnumMethods.GetResourceString(operationResult.MessageStringKey, Language, MessageParameter);

        this.mainOperationResult.DetailResults.Add(operationResult);
    }

    //public Users GetCurrentUser()
    //{
    //    UserBrief userBrief = new UsersBusinessLogic.UsersBusiness(this).GetUserBrief(UserId);

    //    Users users = new Users
    //    {
    //        UserId = UserId,
    //        SurName = UserSurname,
    //        LaboratoryId = CurrentUserLaboratoryId,
    //        DefDivisions = new DefDivisions { DivisionId = CurrentUserDivisionId },
    //        DefLaboratoryDivisions = new DefLaboratoryDivisionsBusiness(this).GetDefLaboratoryDivisions(CurrentUserLaboratoryId, CurrentUserDivisionId),
    //        UserName = UserName,
    //        UserCode = UserCode,
    //        IsADUser = userBrief.IsADUser,
    //        IsActive = userBrief.IsActive,
    //        IsSystemAdministrator = userBrief.IsSystemAdministrator
    //    };

    //    if (userBrief != null)
    //    {
    //        users.UserLabDivisionRoles.Clear();
    //        users.UserLabDivisionRoles.AddRange(userBrief.UserLabDivisionRoles);
    //    }
    //    users.ChangeState(false);
    //    users.SetSystemLanguageEnum(Language);

    //    return users;
    //}

    //public UserBrief GetCurrentUserBrief()
    //{
    //    UserBrief userBrief = new UsersBusinessLogic.UsersBusiness(this).GetUserBrief(UserId);

    //    if (userBrief == null)
    //    {
    //        userBrief = new UserBrief { UserId = UserId, UserName = this.UserName, SurName = this.UserSurname, LaboratoryId = this.CurrentUserLaboratoryId, DivisionId = this.CurrentUserDivisionId };
    //    }
    //    else
    //    {
    //        userBrief.DivisionId = this.CurrentUserDivisionId;
    //    }

    //    return userBrief;
    //}

    public OperationLog GetMainLog()
    {
        return this.mainOperationResult.OperationLog;
    }

    public void SetMainLog(OperationLog operationLog)
    {
        this.mainOperationResult.OperationLog = operationLog;
    }

    internal string GetExceptionMessageFromPotentialExceptions()
    {
        var exceptionOperationResult = this.mainOperationResult.DetailResults.FirstOrDefault(p => p.Exception != null);

        if (exceptionOperationResult != null)
        {
            return exceptionOperationResult.Exception.Message;
        }

        return string.Empty;
    }

    internal List<OperationResult> GetOperationResults()
    {
        return this.mainOperationResult.DetailResults;
    }
}