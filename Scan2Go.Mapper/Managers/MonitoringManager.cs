using Scan2Go.BusinessLogic.MonitoringBusiness;
using Scan2Go.Entity.IdsAndDocuments;
using Scan2Go.Mapper.BaseClasses;
using Utility.Bases;
using Utility.Core;

namespace Scan2Go.Mapper.Managers;

public class MonitoringManager : BaseManager
{
    public MonitoringManager(IUser user) : base(user)
    {
    }

    public async Task<OperationResult> GetMails()
    {
        OperationResult operationResult = new OperationResult();

        IDsAndDocumentsResults idsAndDocumentsResults = await new MonitoringBusiness(operationResult, this.user).GetMails();

        operationResult.ResultObject = idsAndDocumentsResults;
        return operationResult;
    }
}