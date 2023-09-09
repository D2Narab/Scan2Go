using Scan2Go.Entity.BaseClasses;
using Scan2Go.Enums;

namespace Scan2Go.Entity.IdsAndDocuments
{
    public class Passport : IIDsAndDocuments
    {
        [RegulaAttributes(new[] { "fieldName", "Authority", "value" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string Authority { get; set; }

        [RegulaAttributes(new[] { "FieldName", "Date of Birth", "Buf_Length", "12", "Buf_Text" },
            DynamicJSonExtractionType.TwoMainFieldNamesWithTwoValuesAndSecondFieldName)]
        public override string DateOfBirth { get; set; }

        [RegulaAttributes(new[] { "FieldName", "Date of Expiry", "Buf_Length", "12", "Buf_Text" },
            DynamicJSonExtractionType.TwoMainFieldNamesWithTwoValuesAndSecondFieldName)]
        public override string DateOfExpiry { get; set; }

        [RegulaAttributes(new[] { "FieldName", "Date of Issue", "Buf_Length", "12", "Buf_Text" },
            DynamicJSonExtractionType.TwoMainFieldNamesWithTwoValuesAndSecondFieldName)]
        public string DateOfIssue { get; set; }

        [RegulaAttributes(new[] { "DocumentName" }, DynamicJSonExtractionType.MainFieldNameOnly)]
        public string DocumentName { get; set; }

        [RegulaAttributes(new[] { "FieldName", "Ghost portrait", "image", "image" },
        DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldNameWithSubValue)]
        public string GhostPortrait { get; set; }

        [RegulaAttributes(new[] { "fieldName", "Issuing State Name", "value" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string IssuingStateName { get; set; }

        [RegulaAttributes(new[] { "fieldName", "Nationality", "value" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string Nationality { get; set; }

        [RegulaAttributes(new[] { "fieldName", "Place of Birth", "value" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string PlaceOFBirth { get; set; }
    }
}