using Utility.Bases;

namespace Scan2Go.Mapper.Models.DefinitionModels;

public class DefinitionModel : ISelectableItem
{
    public int DefaultValueId { get; set; }
    public IList<DefDetailSchemaModel> defDetailSchemas { get; set; }
    public string DefinitionName { get; set; }
    public string DefinitionTableName { get; set; }
    public bool DoesBelongToParent { get; set; }
    public bool IsActive { get; set; }
    public bool IsDefinable { get; set; }
    public bool IsLanguageIndependent { get; set; }
    public string ParentDefinitionName { get; set; }
    public int ParentDefinitionPkValue { get; set; }
    public int PkValue { get; set; }
    public int PrimaryKeyValue { get; set; }
    public string TableName { get; set; }

    #region ISelectableItem Members

    public int Id => PrimaryKeyValue;

    public string Label
    { get { return DefinitionName; } }

    public string Value
    { get { return DefinitionName; } }

    #endregion ISelectableItem Members
}