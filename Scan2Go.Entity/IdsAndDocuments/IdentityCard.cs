using Scan2Go.Entity.BaseClasses;
using Scan2Go.Enums;

namespace Scan2Go.Entity.IdsAndDocuments
{
    public class IdentityCard : IIDsAndDocuments
    {
        [RegulaAttributes(new[] { "FieldName", "Date of Birth", "Buf_Text" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public override string DateOfBirth { get; set; }

        [RegulaAttributes(new[] { "FieldName", "Date of Expiry", "Buf_Text" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public override string DateOfExpiry { get; set; }

        [RegulaAttributes(new[] { "FieldName", "Ghost portrait", "image", "image" },
            DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldNameWithSubValue)]
        public string GhostPortrait { get; set; }

        [RegulaAttributes(new[] { "fieldName", "Issuing State Name", "value" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string IssuingStateName { get; set; }

        [RegulaAttributes(new[] { "fieldName", "Nationality", "value" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string Nationality { get; set; }
    }
}