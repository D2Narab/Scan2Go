namespace Scan2Go.Entity.Definitions;

public class DefinitionGetEntity
{
    public string CustomFilterFieldName { get; set; }
    public object CustomFilterFieldValue { get; set; }
    public int DefIdToBeExcluded { get; set; }
    public string DefinitionName { get; set; }
    public int? DefinitionPkValue { get; set; }
    public bool IsActive { get; set; }
    public int? ParentDefinitionId { get; set; }
}