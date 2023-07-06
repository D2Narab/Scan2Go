using Scan2Go.Enums.Translations;
using Utility.Core.LogClasses;
using Utility.Enum;

namespace Scan2Go.Enums;

public static class EnumMethods
{
    /// <summary>
    /// Gets the enum Type according the given string name from the Current Assemblies in the working domain  ( thats why it cannot be put on the framework ) .
    /// </summary>
    /// <param name="enumName"></param>
    /// <returns></returns>
    public static Type GetEnumType(this string enumName)
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(p => p.GetName().FullName.StartsWith("Clims.Enums") || p.GetName().FullName.StartsWith("Utility")))
        {
            IEnumerable<string> nameSpaces = assembly.GetTypes().Select(t => t.Namespace).Distinct();

            foreach (var nameSpace in nameSpaces)
            {
                var type = assembly.GetType(nameSpace + "." + enumName);

                if (type == null)
                {
                    continue;
                }

                if (type.IsEnum)
                {
                    return type;
                }
            }
        }

        return null;
    }

    public static string GetResourceString(string messageStrings, LanguageEnum interfaceLanguage, List<MessageParameters> messageParameters = null)
    {
        if (string.IsNullOrEmpty(messageStrings))
        {
            return string.Empty;
        }

        string returnValue = GetStringValueFromTranslations(messageStrings, interfaceLanguage);

        if (messageParameters != null)
        {
            foreach (MessageParameters messageParameter in messageParameters)
            {
                if (messageParameter.Value == null || messageParameter.Key == null)
                {
                    continue;
                }

                string messageParameterValueToBeAdded;

                //if (interfaceLanguage == LanguageEnum.AR && messageParameter.Value.ContainsSpecialCharacters())
                //{
                //    messageParameterValueToBeAdded = messageParameter.Value.ReverseStringSplittingCustomChar();
                //}
                //else
                //{
                messageParameterValueToBeAdded = messageParameter.Value;
                //}

                if (messageParameter.IsEnum == false)
                {
                    returnValue = returnValue?.Replace($"@{Convert.ToString(messageParameter.Key)}", Convert.ToString(messageParameterValueToBeAdded));
                }
                else
                {
                    string enumValue = GetMessageParameterValueFromTranslations(messageParameter, interfaceLanguage);

                    if (string.IsNullOrEmpty(enumValue) == false)
                    {
                        returnValue = returnValue?.Replace($"@{Convert.ToString(messageParameter.Key)}", enumValue);
                    }
                    else
                    {
                        returnValue = returnValue?.Replace($"@{Convert.ToString(messageParameter.Key)}", messageParameterValueToBeAdded);
                    }
                }
            }
        }

        if (string.IsNullOrEmpty(returnValue))
        {
            returnValue = messageStrings;
        }

        return returnValue;
    }

    //public static string GetResourceString(string v, object languageEnum)
    //{
    //    throw new NotImplementedException();
    //}

    public static string GetResourceString(string messageStrings, LanguageEnum interfaceLanguage, MessageParameters messageParameter)
    {
        if (string.IsNullOrEmpty(messageStrings))
        {
            return string.Empty;
        }

        string returnValue = GetStringValueFromTranslations(messageStrings, interfaceLanguage);

        if (messageParameter != null && messageParameter.Value != null && messageParameter.Key != null)
        {
            if (messageParameter.IsEnum == false)
            {
                returnValue = returnValue?.Replace($"@{Convert.ToString(messageParameter.Key)}", Convert.ToString(messageParameter.Value));
            }
            else
            {
                string enumValue = GetMessageParameterValueFromTranslations(messageParameter, interfaceLanguage);

                if (string.IsNullOrEmpty(enumValue) == false)
                {
                    returnValue = returnValue?.Replace($"@{Convert.ToString(messageParameter.Key)}", enumValue);
                }
                else
                {
                    returnValue = returnValue?.Replace($"@{Convert.ToString(messageParameter.Key)}", messageParameter.Value);
                }
            }
        }

        if (string.IsNullOrEmpty(returnValue))
        {
            returnValue = messageStrings;
        }

        return returnValue;
    }

    private static string GetMessageParameterValueFromTranslations(MessageParameters messageParameter, LanguageEnum interfaceLanguage)
    {
        string enumValue;

        switch (interfaceLanguage)
        {
            case LanguageEnum.AR:
                enumValue = TranslationsHandlers.TranslationsHandlersList.FirstOrDefault(p => p.TranslationKey.Equals(messageParameter.Value))?.Arabic;
                break;

            case LanguageEnum.AZ:
                enumValue = TranslationsHandlers.TranslationsHandlersList.FirstOrDefault(p => p.TranslationKey.Equals(messageParameter.Value))?.Azerbaijani;
                break;

            case LanguageEnum.EN:
                enumValue = TranslationsHandlers.TranslationsHandlersList.FirstOrDefault(p => p.TranslationKey.Equals(messageParameter.Value))?.English;
                break;

            case LanguageEnum.TR:
                enumValue = TranslationsHandlers.TranslationsHandlersList.FirstOrDefault(p => p.TranslationKey.Equals(messageParameter.Value))?.Turkish;
                break;

            default:
                enumValue = TranslationsHandlers.TranslationsHandlersList.FirstOrDefault(p => p.TranslationKey.Equals(messageParameter.Value))?.English;
                break;
        }

        return enumValue;
    }

    private static string GetStringValueFromTranslations(string messageStrings, LanguageEnum interfaceLanguage)
    {
        string returnValue;

        switch (interfaceLanguage)
        {
            case LanguageEnum.AR:
                returnValue = TranslationsHandlers.TranslationsHandlersList.FirstOrDefault(p => p.TranslationKey.Equals(messageStrings))?.Arabic;
                break;

            case LanguageEnum.AZ:
                returnValue = TranslationsHandlers.TranslationsHandlersList.FirstOrDefault(p => p.TranslationKey.Equals(messageStrings))?.Azerbaijani;
                break;

            case LanguageEnum.EN:
                returnValue = TranslationsHandlers.TranslationsHandlersList.FirstOrDefault(p => p.TranslationKey.Equals(messageStrings))?.English;
                break;

            case LanguageEnum.TR:
                returnValue = TranslationsHandlers.TranslationsHandlersList.FirstOrDefault(p => p.TranslationKey.Equals(messageStrings))?.Turkish;
                break;

            default:
                returnValue = TranslationsHandlers.TranslationsHandlersList.FirstOrDefault(p => p.TranslationKey.Equals(messageStrings))?.English;
                break;
        }

        return returnValue;
    }
}