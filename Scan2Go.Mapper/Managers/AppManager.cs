using Scan2Go.BusinessLogic.AppBusinessLogic;
using Scan2Go.Entity.Cars;
using Scan2Go.Entity.IdsAndDocuments;
using Scan2Go.Mapper.BaseClasses;
using Scan2Go.Mapper.Models.CarsModels;
using Scan2Go.Mapper.Models.DocumentsModels;
using Utility.Bases;
using Utility.Core;

namespace Scan2Go.Mapper.Managers
{
    public class AppManager : BaseManager
    {
        public AppManager(IUser user) : base(user)
        {
        }

        public async Task<OperationResult> UploadIdDocument(byte[] documentData)
        {
            OperationResult operationResult = new OperationResult();

            IDsAndDocumentsResults idsAndDocumentsResults = await new AppBusiness(operationResult, this.user).ExtractDataFromDocument(documentData);

            IDsAndDocumentsResultsAppModel iDsAndDocumentsResultsAppModel 
                = Mapper.Map<IDsAndDocumentsResults, IDsAndDocumentsResultsAppModel>(idsAndDocumentsResults);

            operationResult.ResultObject = iDsAndDocumentsResultsAppModel;
            return operationResult;
        }
    }
}