using Scan2Go.BusinessLogic.BaseClasses;
using Scan2Go.Enums;
using Scan2Go.Enums.Properties;
using Scan2Go.Enums.Translations;
using Scan2Go.Facade;
using Utility.Bases;
using Utility.Bases.EntityBases;
using Utility.Core;
using Utility.Core.LogClasses;
using Utility.Extensions;

namespace Scan2Go.BusinessLogic.TranslationsBusinessLogic;

public class TranslationsBusiness : BaseBusiness
{
    private TranslationsFacade _translationsFacade;
    private TranslationsValidation _translationsValidation;

    public TranslationsBusiness(OperationResult operationResult, IUser currentUser) : base(operationResult, currentUser)
    {
    }

    public TranslationsBusiness(BaseBusiness baseBusiness) : base(baseBusiness)
    {
    }

    private TranslationsFacade translationsFacade => _translationsFacade ??= new TranslationsFacade(this.Language);
    private TranslationsValidation translationsValidation => _translationsValidation ??= new TranslationsValidation(this);

    public Translations GetTranslation(int translationId)
    {
        return translationsFacade.GetTranslation(translationId);
    }

    public ListSourceBase GetTranslations(int rangeStart, int rangeEnd)
    {
        return translationsFacade.GetTranslations(rangeStart, rangeEnd);
    }

    public ListSourceBase GetTranslationsForList(TranslationSearchCriteria translationSearchCriteria)
    {
        return translationsFacade.GetTranslationsForList(translationSearchCriteria);
    }

    public Translations SaveTranslation(Translations translations)
    {
        bool checkTranslationIsSame = translationsFacade.CheckTranslationIsSame(translations.TranslationKey, translations.TranslationId);

        if (translations.TranslationId == 0)
        {
            translationsValidation.CheckTranslationIsSame(checkTranslationIsSame);
        }

        translationsValidation.CheckTraslationKeyForConvenience(translations.TranslationKey);

        translationsValidation.CheckUsageAreaIsEmpty(translations.EnmTranslationUsageArea);

        if (this.OperationState)
        {
            this.AddDetailResult(translationsFacade.SaveTranslations(translations));
        }

        string operationMessage;
        string operationDetailMessage = string.Empty;

        if (this.OperationState)
        {
            operationMessage = nameof(MessageStrings.saveTranslations);
        }
        else
        {
            operationMessage = nameof(MessageStrings.OperationWasNotCompleted);
            operationDetailMessage = this.GetExceptionMessageFromPotentialExceptions();
        }

        this.SetMainLog(new OperationLog
        {
            ObjectId = translations.TranslationId,
            CaseId = 0,
            ExpertiseId = 0,
            OperationId = (int)Operations.SaveTranslations,
            ModuleId = (int)Modules.Translations,
            OperationMessageResourceId = operationMessage,
        });

        if (this.OperationState == false)
        {
            OperationResult operationResultDetail = new OperationResult(Modules.Translations.AsInt(), Operations.SaveTranslations.AsInt());

            operationResultDetail.OperationLog = new OperationLog
            {
                ObjectId = translations.TranslationId,
                CaseId = 0,
                ExpertiseId = 0,
                OperationId = (int)Operations.SaveTranslations,
                ModuleId = (int)Modules.Translations,
                OperationMessageResourceId = operationDetailMessage
            };

            this.AddDetailResult(operationResultDetail);
        }

        return translations;
    }
}