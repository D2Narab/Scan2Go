using Scan2Go.DataLayer.BaseClasses.SelectOperationBases;
using Scan2Go.DataLayer.TranslationsDataLayer;
using Scan2Go.Enums;
using Scan2Go.Enums.Properties;
using Scan2Go.Enums.Translations;
using System.Data;
using Utility.Bases.EntityBases;
using Utility.Bases.EntityBases.Facade;
using Utility.Core;
using Utility.Enum;
using Utility.Extensions;

namespace Scan2Go.Facade;

public class TranslationsFacade : FacadeBase
{
    public TranslationsFacade(LanguageEnum languageEnum) : base(languageEnum)
    {
    }

    public bool CheckTranslationIsSame(string translationKey, int translationId)
    {
        Scan2GoSelectOperations selectOperations = new Scan2GoSelectOperations();
        DataRow dataRow = selectOperations.GetEntityByAnotherColumnDataRow<Translations>(Translations.Field.TranslationKey, translationKey, DbType.String);

        if (dataRow != null)
        {
            return true;
        }

        return false;
    }

    public Translations GetTranslation(int translationId)
    {
        Scan2GoSelectOperations selectOperations = new Scan2GoSelectOperations();
        DataRow dataRow = selectOperations.GetEntityDataRow<Translations>(translationId);

        return FillTranslations(dataRow);
    }
    //public string GetDateDiffBetween(DateTime? start, DateTime? end)
    //{
    //    string returnValue = string.Empty;

    //    if (start.HasValue && end.HasValue)
    //    {
    //        TimeSpan timeSpan = end.Value.Subtract(start.Value);

    //        if (timeSpan.Days > 0)
    //        {
    //            returnValue = $"{timeSpan.Days} {EnumMethods.GetResourceString(nameof(MessageStrings.Day), this.languageEnum)} ";
    //        }

    //        if (timeSpan.Hours > 0)
    //        {
    //            returnValue += $"{timeSpan.Hours} {EnumMethods.GetResourceString(nameof(MessageStrings.Hour), this.languageEnum)} ";
    //        }

    //        if (timeSpan.Minutes > 0)
    //        {
    //            returnValue += $"{timeSpan.Minutes} {EnumMethods.GetResourceString(nameof(MessageStrings.Minute), this.languageEnum)} ";
    //        }

    //        if (returnValue == string.Empty && timeSpan.Seconds > 0)
    //        {
    //            returnValue += $"{EnumMethods.GetResourceString(nameof(MessageStrings.LessThanMinute), this.languageEnum)}  ";
    //        }
    //    }

    public ListSourceBase GetTranslations(int rangeStart = 0, int rangeEnd = 0)
    {
        ListSourceBase listSourceBase = new ListSourceBase();
        listSourceBase.ListItemBases = new List<ListItemBase>();
        listSourceBase.RecordInfo = EnumMethods.GetResourceString(nameof(MessageStrings.ListTranslations), this.languageEnum);
        listSourceBase.ListItemType = typeof(Translations);

        if (TranslationsHandlers.TranslationsHandlersList != null && TranslationsHandlers.TranslationsHandlersList.Any())
        {
            if (rangeStart == 0 && rangeEnd == 0)
            {
                listSourceBase.ListItemBases = TranslationsHandlers.TranslationsHandlersListItems;
            }
            else
            {
                listSourceBase.ListItemBases = TranslationsHandlers.TranslationsHandlersListItems.GetRange(rangeStart, rangeEnd - rangeStart);
            }

            listSourceBase.TotalRecordCount = TranslationsHandlers.TranslationsHandlersListItems.Count;

            return listSourceBase;
        }

        DataTable dataTable = new Scan2GoSelectOperations().GetEntityDataTable<Translations>();

        foreach (DataRow dataRow in dataTable.Rows)
        {
            Translations translations = FillTranslations(dataRow);
            TranslationsHandlers.TranslationsHandlersList.Add(translations);
            listSourceBase.ListItemBases.Add(translations.ListItemMember);
        }

        return listSourceBase;
    }

    //    return returnValue;
    //}
    public ListSourceBase GetTranslationsForList(TranslationSearchCriteria translationSearchCriteria)
    {
        CriteriaResult criteriaResult = new TranslationsDAO().GetTranslationsForList(translationSearchCriteria);

        ListSourceBase listSourceBase = new ListSourceBase();

        listSourceBase.ListItemBases = new List<ListItemBase>();
        listSourceBase.TotalRecordCount = criteriaResult.TotalRecordCount;
        listSourceBase.ListItemType = typeof(TranslationsListItem);

        //listSourceBase.RecordInfo = EnumMethods.GetResourceString(nameof(MessageStrings.SearchedCases), this.languageEnum);

        foreach (DataRow dr in criteriaResult.CriteriaDataTable.Rows)
        {
            TranslationsListItem translationsListItem = FillTranslationsListItem(dr);

            if (translationsListItem != null)
            {
                listSourceBase.ListItemBases.Add(translationsListItem);
            }
        }

        listSourceBase.ListCaptionBases = new ListCaptionBasesFacade(this.languageEnum).GetCaptions(listSourceBase);

        return listSourceBase;
    }
    public OperationResult SaveTranslations(Translations translations)
    {
        OperationResult operationResult = new TranslationsDAO().SaveTranslations(translations);
        return operationResult;
    }

    private Translations FillTranslations(DataRow row)
    {
        if (row == null) { return null; }

        var translations = new Translations();

        translations.Arabic = row.AsString(Translations.Field.Arabic);
        translations.Azerbaijani = row.AsString(Translations.Field.Azerbaijani);
        translations.English = row.AsString(Translations.Field.English);
        translations.EnmTranslationUsageArea = (TranslationUsageArea)row.AsInt(Translations.Field.UsageAreaId, true);
        translations.TranslationId = row.AsInt(Translations.Field.TranslationId);
        translations.TranslationKey = row.AsString(Translations.Field.TranslationKey);
        translations.Turkish = row.AsString(Translations.Field.Turkish);

        translations.ChangeState(false);

        return translations;
    }

    private TranslationsListItem FillTranslationsListItem(DataRow row)
    {
        if (row == null) { return null; }

        var translations = new TranslationsListItem();

        translations.Arabic = row.AsString(Translations.Field.Arabic);
        translations.Azerbaijani = row.AsString(Translations.Field.Azerbaijani);
        translations.English = row.AsString(Translations.Field.English);
        translations.EnmTranslationUsageArea = (TranslationUsageArea)row.AsInt(Translations.Field.UsageAreaId, true);
        translations.TranslationId = row.AsInt(Translations.Field.TranslationId);
        translations.TranslationKey = row.AsString(Translations.Field.TranslationKey);
        translations.Turkish = row.AsString(Translations.Field.Turkish);
        translations.EnmTranslationUsageAreaName = EnumMethods.GetResourceString(Enum.GetName(typeof(TranslationUsageArea), row.AsInt(Translations.Field.UsageAreaId)), this.languageEnum);

        return translations;
    }
}