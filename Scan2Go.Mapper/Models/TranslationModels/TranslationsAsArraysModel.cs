namespace Scan2Go.Mapper.Models.TranslationModels;

public class TranslationsAsArraysModel
{
    public Dictionary<string, string> Arabic { get; set; } = new();
    public Dictionary<string, string> Azerbaijani { get; set; } = new();
    public Dictionary<string, string> English { get; set; } = new();
    public Dictionary<string, string> Turkish { get; set; } = new();
}