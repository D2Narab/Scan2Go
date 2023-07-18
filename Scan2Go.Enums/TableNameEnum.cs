using Utility.Core.DataLayer;

namespace Scan2Go.Enums;

public class TableName : DataLayerEnumBase
{
    public static readonly DataLayerEnumBase Def_Definitions = new TableName("Def_Definitions");
    public static readonly DataLayerEnumBase Def_Definitions_Language = new TableName("Def_Definitions_Language");
    public static readonly DataLayerEnumBase Def_Detail_Schema = new TableName("Def_Detail_Schema");
    public static readonly DataLayerEnumBase Translations = new TableName("Translations");
    public static readonly DataLayerEnumBase Users = new TableName("Users");

    public TableName(string internalValue) : base(internalValue)
    {
    }

    public static implicit operator string(TableName dataLayerEnumBase)
    {
        return dataLayerEnumBase.InternalValue;
    }
}
