using Utility.Bases.EntityBases;

namespace Scan2Go.Enums.Translations;

public static class TranslationsHandlers
{
    public static List<Translations> TranslationsHandlersList { get; set; } = new();

    public static List<ListItemBase> TranslationsHandlersListItems
    {
        get
        {
            List<ListItemBase> translationsListItems = new List<ListItemBase>();

            foreach (Translations translations in TranslationsHandlersList)
            {
                translationsListItems.Add(new TranslationsListItem
                {
                    TranslationId = translations.TranslationId,
                    TranslationKey = translations.TranslationKey,
                    Arabic = translations.Arabic,
                    Azerbaijani = translations.Azerbaijani,
                    English = translations.English,
                    Turkish = translations.Turkish,
                    EnmTranslationUsageArea = translations.EnmTranslationUsageArea
                });
            }

            return translationsListItems;
        }
    }

    public static void ResetTranslationHandlersList()
    {
        TranslationsHandlersList = new List<Translations>();
    }

    public static void SetTranslationChangedWords(Translations newTranslations)
    {
        TranslationsHandlersList.FirstOrDefault(p =>
            p.TranslationId == newTranslations.TranslationId).English = newTranslations.English;

        TranslationsHandlersList.FirstOrDefault(p =>
            p.TranslationId == newTranslations.TranslationId).Arabic = newTranslations.Arabic;

        TranslationsHandlersList.FirstOrDefault(p =>
            p.TranslationId == newTranslations.TranslationId).Azerbaijani = newTranslations.Azerbaijani;

        TranslationsHandlersList.FirstOrDefault(p =>
            p.TranslationId == newTranslations.TranslationId).Turkish = newTranslations.Turkish;
    }
}