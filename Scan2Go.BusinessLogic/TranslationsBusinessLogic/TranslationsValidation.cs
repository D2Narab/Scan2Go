using Scan2Go.BusinessLogic.BaseClasses;
using Scan2Go.Enums;
using Scan2Go.Enums.Properties;
using Utility.Core;
using Utility.Extensions;

namespace Scan2Go.BusinessLogic.TranslationsBusinessLogic;

internal class TranslationsValidation
{
    private readonly BaseBusiness baseBusiness;

    public TranslationsValidation(BaseBusiness baseBusiness)
    {
        this.baseBusiness = baseBusiness;
    }

    public void CheckTranslationIsSame(bool checkTranslationIsSame)
    {
        if (checkTranslationIsSame)
        {
            this.baseBusiness.AddDetailResult(new OperationResult { State = false, MessageStringKey = nameof(MessageStrings.AlreadyHaveTheSame) });
        }
    }

    public void CheckTraslationKeyForConvenience(string translationKey)
    {
        if (string.IsNullOrEmpty(translationKey))
        {
            this.baseBusiness.AddDetailResult(new OperationResult { State = false, MessageStringKey = nameof(MessageStrings.TranslationKeyCannotBeEmpty) });
        }
        else if (translationKey.ContainsSpecialCharactersAndNumbers())
        {
            this.baseBusiness.AddDetailResult(new OperationResult { State = false, MessageStringKey = nameof(MessageStrings.TranslationKeyCannotContainSpecialCharacters) });
        }
        else if (!translationKey.ContainsLatinLetters())
        {
            this.baseBusiness.AddDetailResult(new OperationResult { State = false, MessageStringKey = nameof(MessageStrings.TranslationKeyMustBeInLatinLetters) });
        }
    }

    public void CheckUsageAreaIsEmpty(TranslationUsageArea translationUsageArea)
    {
        if ((int)translationUsageArea == 0)
        {
            this.baseBusiness.AddDetailResult(new OperationResult { State = false, MessageStringKey = nameof(MessageStrings.UsageAreaCannotBeEmpty) });
        }
    }
}