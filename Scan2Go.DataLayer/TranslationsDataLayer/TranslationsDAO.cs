using Scan2Go.DataLayer.BaseClasses.DataLayersBases;
using Scan2Go.Enums.Translations;
using Utility.Bases.EntityBases;
using Utility.Core;

namespace Scan2Go.DataLayer.TranslationsDataLayer;

public class TranslationsDAO : Scan2GoDataLayerBase
{
    public TranslationsDAO()
    {
    }

    public CriteriaResult GetTranslationsForList(TranslationSearchCriteria translationSearchCriteria)
    {
        return new TranslationsDMLOperations(this).GetTranslationsForList(translationSearchCriteria);
    }

    public OperationResult SaveTranslations(Translations translations)
    {
        OperationResult operationResult = new OperationResult();

        try
        {
            this.BeginTransaction();

            new TranslationsDMLOperations(this).SaveTranslations(translations);

            TranslationsHandlers.ResetTranslationHandlersList();

            operationResult.State = this.CommitTransaction();
        }
        catch (Exception exception)
        {
            operationResult.State = false;
            operationResult.Exception = exception;
            this.RollbackTransaction();
        }

        return operationResult;
    }
}