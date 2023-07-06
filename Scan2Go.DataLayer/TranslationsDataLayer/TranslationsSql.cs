using DataLayer.Base.SqlGenerator;
using Scan2Go.Enums;
using Scan2Go.Enums.Translations;
using Utility.Bases.EntityBases;

namespace Scan2Go.DataLayer.TranslationsDataLayer;

public class TranslationsSql
{
    public static SqlInsertFactory SaveTranslations(Translations translations)
    {
        SqlInsertFactory mainFactory = new SqlInsertFactory(translations);
        //INSERT - VALUES - PARAMS to be added
        return mainFactory;
    }

    public SqlSelectFactory GetTranslationsForList(TranslationSearchCriteria translationSearchCriteria)
    {
        SqlSelectFactory sqlSelectFactory = new SqlSelectFactory();

        sqlSelectFactory.SelectQuery.Append($" SELECT  COUNT(*) OVER() AS {CriteriaResult.Field.TotalRecordCount} , * ");
        sqlSelectFactory.FromQuery.Append($" FROM {TableName.Translations.InternalValue} WHERE 1 = 1 ");

        if (!string.IsNullOrEmpty(translationSearchCriteria.TranslationKey))
        {
            sqlSelectFactory.WhereQuery.AppendLine($" AND  {TableName.Translations.InternalValue}.{Translations.Field.TranslationKey} LIKE N'%{translationSearchCriteria.TranslationKey}%'");
        }

        if (!string.IsNullOrEmpty(translationSearchCriteria.Turkish))
        {
            sqlSelectFactory.WhereQuery.AppendLine($" AND  {TableName.Translations.InternalValue}.{Translations.Field.Turkish} LIKE N'%{translationSearchCriteria.Turkish}%'");
        }

        if (!string.IsNullOrEmpty(translationSearchCriteria.English))
        {
            sqlSelectFactory.WhereQuery.AppendLine($" AND  {TableName.Translations.InternalValue}.{Translations.Field.English} LIKE N'%{translationSearchCriteria.English}%'");
        }

        if (!string.IsNullOrEmpty(translationSearchCriteria.Arabic))
        {
            sqlSelectFactory.WhereQuery.AppendLine($" AND  {TableName.Translations.InternalValue}.{Translations.Field.Arabic} LIKE N'%{translationSearchCriteria.Arabic}%'");
        }

        if (!string.IsNullOrEmpty(translationSearchCriteria.Azerbaijani))
        {
            sqlSelectFactory.WhereQuery.AppendLine($" AND  {TableName.Translations.InternalValue}.{Translations.Field.Azerbaijani} LIKE N'%{translationSearchCriteria.Azerbaijani}%'");
        }

        if (translationSearchCriteria.EnmTranslationUsageArea != null)
        {
            sqlSelectFactory.WhereQuery.AppendLine($" AND  {TableName.Translations.InternalValue}.{Translations.Field.UsageAreaId} = '{(int)translationSearchCriteria.EnmTranslationUsageArea}'");
        }

        sqlSelectFactory.OrderByQuery.Append($" ORDER BY TranslationId DESC ");

        sqlSelectFactory.OrderByQuery.Append($" OFFSET {translationSearchCriteria.StartFrom} ROWS FETCH NEXT {translationSearchCriteria.Range} ROWS ONLY OPTION (RECOMPILE)");
        return sqlSelectFactory;
    }
}