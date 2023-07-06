namespace Scan2Go.Entity.Definitions;

public class DefinitionTableCreation
{
    public string ArabicName { get; set; }
    public string DefinitionName { get; set; }
    public string EnglishName { get; set; }
    public string AzerbaijaniName { get; set; }
    public string TurkishName { get; set; }
    public bool IsActive { get; set; }
    public bool IsDefinable { get; set; }
    public bool IsLanguageIndependent { get; set; }
    public int DefinitionId { get; set; }
}