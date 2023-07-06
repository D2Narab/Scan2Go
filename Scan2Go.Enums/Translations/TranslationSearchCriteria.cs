using Utility.Bases.EntityBases;

namespace Scan2Go.Enums.Translations
{
    public class TranslationSearchCriteria : CriteriaBase
    {
        public string Arabic { get; set; }
        public string Azerbaijani { get; set; }
        public string English { get; set; }
        public TranslationUsageArea? EnmTranslationUsageArea { get; set; }
        public string TranslationKey { get; set; }
        public string Turkish { get; set; }
    }
}