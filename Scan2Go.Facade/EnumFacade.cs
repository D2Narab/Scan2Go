using Scan2Go.Enums;
using Utility;
using Utility.Bases.EntityBases.Facade;

namespace Scan2Go.Facade;

public class EnumFacade : FacadeBase
{
    public EnumFacade(Utility.Enum.LanguageEnum languageEnum) : base(languageEnum)
    {
    }

    public IList<CustomDataHandler> GetEnum(Type enumType, bool WillBeOrdered = true)
    {
        IList<CustomDataHandler> enumModelList = new List<CustomDataHandler>();

        if (enumType != null)
        {
            foreach (Enum MyEnum in Enum.GetValues(enumType))
            {
                CustomDataHandler oCustomDataHandler = new CustomDataHandler();

                oCustomDataHandler.ID = (int)Enum.Parse(enumType, MyEnum.ToString());
                oCustomDataHandler.NameValue = EnumMethods.GetResourceString(MyEnum.ToString(), this.languageEnum);

                enumModelList.Add(oCustomDataHandler);
            }
        }

        if (WillBeOrdered)
            return enumModelList.OrderBy(p => p.NameValue).ToList();
        else
            return enumModelList.OrderBy(p => p.ID).ToList();
    }

    //public IList<Definition> GetEnumDefinition(Type enumType, DefDetailSchema defDetailSchema)
    //{
    //    IList<Definition> enumModelList = new List<Definition>();

    //    foreach (Enum MyEnum in Enum.GetValues(enumType))
    //    {
    //        Definition odefinition = new Definition();
    //        odefinition.defDetailSchemas.Add(new DefDetailSchema { FieldName = defDetailSchema.FieldName, FieldValue = (int)Enum.Parse(enumType, MyEnum.ToString()), IsPk = true});
    //        odefinition.defDetailSchemas.Add(new DefDetailSchema { FieldName = defDetailSchema.FieldName+"Name", FieldValue = EnumMethods.GetResourceString(MyEnum.ToString(), this.languageEnum),IsNameField=true });

    //        enumModelList.Add(odefinition);
    //    }

    //    return enumModelList.OrderBy(p => p.NameValue).ToList();
    //}
}