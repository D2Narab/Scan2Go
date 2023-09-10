using Scan2Go.Entity.BaseClasses;
using Scan2Go.Enums;

namespace Scan2Go.Entity.IdsAndDocuments
{
    public class Visa : IIDsAndDocuments
    {
        [RegulaAttributes(new[] { "fieldName", "Date of Birth", "value" },
            DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public override string DateOfBirth { get; set; }

        [RegulaAttributes(new[] { "FieldName", "Date of Expiry", "Buf_Length", "9", "Buf_Text" },
            DynamicJSonExtractionType.TwoMainFieldNamesWithTwoValuesAndSecondFieldName)]
        public override string DateOfExpiry { get; set; }

        [RegulaAttributes(new[] { "fieldName", "Date of Issue", "value" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string DateOfIssue { get; set; }

        [RegulaAttributes(new[] { "FieldName", "Duration of Stay", "Buf_Text" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string DurationOfStay { get; set; }

        [RegulaAttributes(new[] { "fieldName", "Issuing State Name", "value" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string IssuingStateName { get; set; }

        [RegulaAttributes(new[] { "fieldName", "Nationality", "value" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string Nationality { get; set; }
        [RegulaAttributes(new[] { "FieldName", "Passport Number", "Buf_Text" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string PassportNumber { get; set; }
        [RegulaAttributes(new[] { "FieldName", "Visa Number", "Buf_Text" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string VisaNumber { get; set; }
        [RegulaAttributes(new[] { "fieldName", "Visa Valid From", "value" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string VisaValidFrom { get; set; }
    }
}