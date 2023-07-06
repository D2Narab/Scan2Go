using DataLayer.Base.DatabaseFactory;
using DataLayer.Base.GeneralDataLayer;
using Scan2Go.DataLayer.BaseClasses.DataLayersBases;
using Scan2Go.Enums.Translations;
using Utility.Bases.EntityBases;

namespace Scan2Go.DataLayer.TranslationsDataLayer;

internal class TranslationsDMLOperations : Scan2GoDataLayerBase
{
    public TranslationsDMLOperations(DataLayerBase dataLayerBase) : base(dataLayerBase)
    {
    }

    public CriteriaResult GetTranslationsForList(TranslationSearchCriteria translationSearchCriteria)
    {
        return new TranslationsSelectOperations(this).GetTranslationsForList(translationSearchCriteria);
    }

    internal void SaveTranslations(Translations translations)
    {
        new GeneralDMLOperations(this).SaveEntity(translations);
    }
}