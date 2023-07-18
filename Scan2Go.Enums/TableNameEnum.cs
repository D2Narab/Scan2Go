using Utility.Core.DataLayer;

namespace Scan2Go.Enums;

public class TableName : DataLayerEnumBase
{
    public static readonly DataLayerEnumBase Def_Definitions = new TableName("Def_Definitions");
    public static readonly DataLayerEnumBase Def_Definitions_Language = new TableName("Def_Definitions_Language");
    public static readonly DataLayerEnumBase Def_Detail_Schema = new TableName("Def_Detail_Schema");
    public static readonly DataLayerEnumBase Translations = new TableName("Translations");
    public static readonly DataLayerEnumBase Users = new TableName("Users");
    public static readonly DataLayerEnumBase Cars = new TableName("Cars");
    public static readonly DataLayerEnumBase Def_CarBrands = new TableName("Def_CarBrands");
    public static readonly DataLayerEnumBase Def_CarModels = new TableName("Def_CarModels");

    public TableName(string internalValue) : base(internalValue)
    {
    }

    public static implicit operator string(TableName dataLayerEnumBase)
    {
        return dataLayerEnumBase.InternalValue;
    }
}
