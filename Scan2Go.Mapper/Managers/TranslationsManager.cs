using Scan2Go.BusinessLogic.TranslationsBusinessLogic;
using Scan2Go.Enums;
using Scan2Go.Enums.Translations;
using Scan2Go.Mapper.BaseClasses;
using Scan2Go.Mapper.Models.EnumModels;
using Scan2Go.Mapper.Models.TranslationModels;
using Utility.Bases.EntityBases;
using Utility.Core;

namespace Scan2Go.Mapper.Managers;

public class TranslationsManager : BaseManager
{
    public TranslationsManager() : base()
    {
    }

    public OperationResult GetTranslation(int translationId)
    {
        OperationResult operationResult = new OperationResult();

        TranslationsBusiness translationsBusiness = new TranslationsBusiness(operationResult);

        Translations translations = translationsBusiness.GetTranslation(translationId);

        operationResult.ResultObject = Mapper.Map<Translations, TranslationsModel>(translations);

        return operationResult;
    }

    public OperationResult GetTranslations(int rangeStart, int rangeEnd)
    {
        OperationResult operationResult = new OperationResult();

        ListSourceBase translationListItems = new TranslationsBusiness(operationResult).GetTranslations(rangeStart, rangeEnd);

        operationResult.ResultObject = Mapper.Map<ListSourceBase, ListSourceModel<TranslationsModel>>(translationListItems);

        return operationResult;
    }

    public OperationResult GetTranslations(EnumModel<TranslationUsageArea> enmTranslationUsageArea)
    {
        OperationResult operationResult = new OperationResult();

        TranslationUsageArea translationUsageArea = Mapper.Map<EnumModel<TranslationUsageArea>, TranslationUsageArea>(enmTranslationUsageArea);

        ListSourceModel<TranslationsModel> translationsListSourceModel = GetTranslations(0, 0).ResultObject;

        List<TranslationsModel> translationsModels = (List<TranslationsModel>)translationsListSourceModel.ListItemBases;

        if (translationUsageArea == TranslationUsageArea.Backend || translationUsageArea == TranslationUsageArea.Frontend)
        {
            translationsModels = translationsModels.Where(p => p.EnmTranslationUsageArea == translationUsageArea).ToList();

            translationsListSourceModel.ListItemBases = translationsModels;
        }

        TranslationsAsArraysModel translationsAsArraysModel = new TranslationsAsArraysModel();

        foreach (TranslationsModel translationsModel in translationsModels)
        {
            if (translationsAsArraysModel.Arabic.ContainsKey(translationsModel.TranslationKey))
                continue;

            translationsAsArraysModel.Arabic.Add(translationsModel.TranslationKey, translationsModel.Arabic);
            translationsAsArraysModel.Azerbaijani.Add(translationsModel.TranslationKey, translationsModel.Azerbaijani);
            translationsAsArraysModel.English.Add(translationsModel.TranslationKey, translationsModel.English);
            translationsAsArraysModel.Turkish.Add(translationsModel.TranslationKey, translationsModel.Turkish);
        }

        operationResult.ResultObject = translationsAsArraysModel;
        return operationResult;
    }

    public OperationResult GetTranslationsForList(TranslationSearchCriteriaModel translationSearchCriteriaModel)
    {
        OperationResult operationResult = new OperationResult();

        TranslationSearchCriteria translationSearchCriteria = Mapper.Map<TranslationSearchCriteriaModel, TranslationSearchCriteria>(translationSearchCriteriaModel);

        ListSourceBase translationListItems = new TranslationsBusiness(operationResult).GetTranslationsForList(translationSearchCriteria);

        operationResult.ResultObject = Mapper.Map<ListSourceBase, ListSourceModel<TranslationsModel>>(translationListItems);

        return operationResult;
    }
    public OperationResult SaveTranslations(TranslationsModel translationsModel)
    {
        Translations translations = Mapper.Map<TranslationsModel, Translations>(translationsModel);

        OperationResult operationResult = new OperationResult((int)Modules.Translations, (int)Operations.SaveTranslations);
        Translations translationsUpdated = new TranslationsBusiness(operationResult).SaveTranslation(translations);
        TranslationsModel translationsModelUpdatedModel = Mapper.Map<Translations, TranslationsModel>(translationsUpdated);

        operationResult.ResultObject = translationsModelUpdatedModel;

        return operationResult;
    }
}