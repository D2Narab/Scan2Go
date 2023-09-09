using Scan2Go.Entity.BaseClasses;
using Scan2Go.Enums;

namespace Scan2Go.Entity.IdsAndDocuments
{
    public class DrivingLicense : IIDsAndDocuments
    {
        [RegulaAttributes(new[] { "FieldName", "Date of Birth", "Buf_Length", "11", "Buf_Text" },
            DynamicJSonExtractionType.TwoMainFieldNamesWithTwoValuesAndSecondFieldName)]
        public override string DateOfBirth { get; set; }

        [RegulaAttributes(new[] { "FieldName", "Date of Expiry", "Buf_Length", "11", "Buf_Text" },
                    DynamicJSonExtractionType.TwoMainFieldNamesWithTwoValuesAndSecondFieldName)]
        public override string DateOfExpiry { get; set; }

        [RegulaAttributes(new[] { "FieldName", "First Issue Date", "Buf_Length", "11", "Buf_Text" },
            DynamicJSonExtractionType.TwoMainFieldNamesWithTwoValuesAndSecondFieldName)]
        public string DateOfIssue { get; set; }

        [RegulaAttributes(new[] { "FieldName", "DL Class", "Buf_Text" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string DlCategory { get; set; }

        [RegulaAttributes(new[] { "DocumentName" }, DynamicJSonExtractionType.MainFieldNameOnly)]
        public string DocumentName { get; set; }

        [RegulaAttributes(new[] { "fieldName", "Issuing State Name", "value" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string IssuingStateName { get; set; }

        [RegulaAttributes(new[] { "fieldName", "Place of Birth", "value" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string PlaceOFBirth { get; set; }

        [RegulaAttributes(new[] { "FieldName", "Place of Issue", "Buf_Text" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string PlaceOfIssue { get; set; }
    }
}