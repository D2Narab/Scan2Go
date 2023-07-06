using Scan2Go.Mapper.Models.EnumModels;
using Utility;
using Utility.Enum.GeneralEnums;

namespace Scan2Go.Mapper.Models.DefinitionModels;

public class DefDetailSchemaModel
{
    public IList<CustomDataHandler> DataSourceList { get; set; }
    public string DataSourceServicePath { get; set; }
    public string DefName { get; set; }
    public int DefSchemaId { get; set; }
    public EnumModel<InterfaceComponentType> EnmInterfaceComponentType { get; set; }
    public EnumModel<MandatoryStatus> EnmMandatoryStatus { get; set; }
    public string FieldInterfaceLabel { get; set; }
    public string FieldName { get; set; }
    public object FieldValue { get; set; }
    public string InterfaceComponentTypeName { get; set; }
    public bool IsLabelVisible { get; set; }
    public bool IsMultiLanguage { get; set; }
    public bool IsNameField { get; set; }

    //public bool IsParent { get; set; }
    public bool IsPk { get; set; }

    public int Length { get; set; }
    public string MandatoryStatusName { get; set; }
    public string MessageText { get; set; }
    public int OrderNumber { get; set; }
    public CustomDataHandler ParentDefinition { get; set; }
    public bool ReturnPkSchemaMember { get; set; } = true;
    public int SubFieldSchemaId { get; set; }
    public DefDetailSchemaModel SubSchemaObject { get; set; }
}