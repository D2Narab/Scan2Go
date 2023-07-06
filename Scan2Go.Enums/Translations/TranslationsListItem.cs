using Utility.Bases.EntityBases;

namespace Scan2Go.Enums.Translations
{
    public class TranslationsListItem : ListItemBase
    {
        public string Arabic { get; set; }
        public string Azerbaijani { get; set; }
        public string English { get; set; }
        public TranslationUsageArea EnmTranslationUsageArea { get; set; }
        public string EnmTranslationUsageAreaName { get; set; }
        public int TranslationId { get; set; }
        public string TranslationKey { get; set; }
        public string Turkish { get; set; }
        #region ListItemBase Members

        public override int RowId => TranslationId;

        #endregion ListItemBase Members
    }
}