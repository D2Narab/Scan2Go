using Scan2Go.BusinessLogic.MonitoringBusiness;
using Scan2Go.Entity.IdsAndDocuments;
using Utility.Core;

namespace Scan2Go.Mapper.Managers;

public class MonitoringManager
{
    public async Task<OperationResult> GetMails()
    {
        OperationResult operationResult = new OperationResult();

        IDsAndDocumentsResults idsAndDocumentsResults = await new MonitoringBusiness().GetMails();

        operationResult.ResultObject = idsAndDocumentsResults;
        return operationResult;
    }
}