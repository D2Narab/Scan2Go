using Scan2Go.Entity.BaseClasses;
using Scan2Go.Enums;
using Utility.Extensions;

namespace Scan2Go.Entity.IdsAndDocuments;

public class IIDsAndDocuments
{
    public virtual string DateOfBirth { get; set; }
    public virtual string DateOfExpiry { get; set; }

    public string DocumentCategory { get; set; }

    [RegulaAttributes(new[] { "fieldName", "Document front side", "valueList", "value" },
        DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldNameWithSubValue)]
    public string DocumentFrontSide { get; set; }

    [RegulaAttributes(new[] { "FieldName", "Document Number", "Buf_Text" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
    public string DocumentNumber { get; set; }

    [RegulaAttributes(new[] { "DocumentName" }, DynamicJSonExtractionType.MainFieldNameOnly)]
    public string DocumentType { get; set; }

    [RegulaAttributes(new[] { "FieldName", "Given Names", "Buf_Text" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
    public string Name { get; set; }

    [RegulaAttributes(new[] { "FieldName", "Personal Number", "Buf_Text" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
    public string PersonalNumber { get; set; }

    [RegulaAttributes(new[] { "FieldName", "Portrait", "image", "image" },
        DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldNameWithSubValue)]
    public string PortraitImage { get; set; }

    public ScannedDocumentType ScannedDocumentType { get; set; }

    [RegulaAttributes(new[] { "FieldName", "Signature", "image", "image" },
    DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldNameWithSubValue)]
    public string Signature { get; set; }

    [RegulaAttributes(new[] { "FieldName", "Surname", "Buf_Text" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
    public string Surname { get; set; }

    #region Other Properties

    public int Age
    {
        get
        {
            TimeSpan span = DateTime.Now - DateOfBirth.AsDateTime();
            double totalYears = span.TotalDays / 365.25;

            return (int)Math.Floor(totalYears);
        }
    }

    public bool IsExpired
    {
        get
        {
            if (DateOfExpiry.AsDateTime() < DateTime.Now)
            {
                return true;
            }

            return false;
        }
    }

    #endregion Other Properties
}