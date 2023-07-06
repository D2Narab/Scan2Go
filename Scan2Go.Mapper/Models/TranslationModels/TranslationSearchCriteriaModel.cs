using Scan2Go.Enums;
using Scan2Go.Mapper.Models.EnumModels;

namespace Scan2Go.Mapper.Models.TranslationModels
{
    public class TranslationSearchCriteriaModel
    {
        public string Arabic { get; set; }
        public string Azerbaijani { get; set; }
        public string English { get; set; }
        public EnumModel<TranslationUsageArea> EnmTranslationUsageArea { get; set; }
        public string TranslationKey { get; set; }
        public string Turkish { get; set; }

        #region CriteriaBaseMembers

        public string OrderByColumn { get; set; }
        public int Range { get; set; }
        public string SortType { get; set; }
        public int StartFrom { get; set; }

        #endregion CriteriaBaseMembers
    }
}