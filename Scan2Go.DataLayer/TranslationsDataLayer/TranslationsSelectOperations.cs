using DataLayer.Base.DatabaseFactory;
using DataLayer.Base.SqlGenerator;
using Scan2Go.DataLayer.BaseClasses.SelectOperationBases;
using Scan2Go.Enums.Translations;
using System.Data;
using Utility.Bases.EntityBases;
using Utility.Core.DataLayer;

namespace Scan2Go.DataLayer.TranslationsDataLayer;

public class TranslationsSelectOperations : Scan2GoSelectOperations
{
    public TranslationsSelectOperations(DataLayerBase dataLayerBase) : base(dataLayerBase)
    {
    }

    public CriteriaResult GetTranslationsForList(TranslationSearchCriteria translationSearchCriteria)
    {
        SqlSelectFactory sqlSelectFactory = new SqlSelectFactory();

        sqlSelectFactory = new TranslationsSql().GetTranslationsForList(translationSearchCriteria);

        sqlSelectFactory.AddParam(new DatabaseParameter { FieldName = Translations.Field.TranslationKey, DbType = DbType.String, ParameterDirection = ParameterDirection.Input, FieldValue = translationSearchCriteria.TranslationKey });

        return this.GetCriteriaResult(sqlSelectFactory);
    }
}