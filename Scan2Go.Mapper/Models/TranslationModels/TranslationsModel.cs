using Scan2Go.Enums;
using Scan2Go.Mapper.Models.EnumModels;

namespace Scan2Go.Mapper.Models.TranslationModels;

public class TranslationsModel
{
    public string Arabic { get; set; }
    public string Azerbaijani { get; set; }

    public string English { get; set; }

    public EnumModel<TranslationUsageArea> EnmTranslationUsageArea { get; set; }
    public int TranslationId { get; set; }
    public string EnmTranslationUsageAreaName { get; set; }

    public string TranslationKey { get; set; }
    public string Turkish { get; set; }
}