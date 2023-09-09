using Scan2Go.Entity.BaseClasses;
using Scan2Go.Enums;
using Utility.Extensions;

namespace Scan2Go.Entity.IdsAndDocuments
{
    public class DrivingLicense : IIDsAndDocuments
    {
        [RegulaAttributes(new[] { "FieldName", "Date of Birth", "Buf_Length", "11", "Buf_Text" },
            DynamicJSonExtractionType.TwoMainFieldNamesWithTwoValuesAndSecondFieldName)] 
        public string DateOfBirth { get; set; }

        [RegulaAttributes(new[] { "FieldName", "Date of Expiry", "Buf_Length", "11", "Buf_Text" },
                    DynamicJSonExtractionType.TwoMainFieldNamesWithTwoValuesAndSecondFieldName)]
        public string DateOfExpiry { get; set; }

        [RegulaAttributes(new[] { "FieldName", "First Issue Date", "Buf_Length", "11", "Buf_Text" },
            DynamicJSonExtractionType.TwoMainFieldNamesWithTwoValuesAndSecondFieldName)]
        public string DateOfIssue { get; set; }

        [RegulaAttributes(new[] { "FieldName", "DL Class", "Buf_Text" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string DlCategory { get; set; }

        public string DocumentCategory { get; set; }

        [RegulaAttributes(new[] { "fieldName", "Document front side", "valueList", "value" },
            DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldNameWithSubValue)]
        public string DocumentFrontSide { get; set; }

        [RegulaAttributes(new[] { "DocumentName" }, DynamicJSonExtractionType.MainFieldNameOnly)]
        public string DocumentName { get; set; }

        [RegulaAttributes(new[] { "FieldName", "Document Number", "Buf_Text" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string DocumentNumber { get; set; }
        [RegulaAttributes(new[] { "fieldName", "Issuing State Name", "value" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string IssuingStateName { get; set; }

        [RegulaAttributes(new[] { "FieldName", "Given Names", "Buf_Text" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string Name { get; set; }

        [RegulaAttributes(new[] { "FieldName", "Personal Number", "Buf_Text" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string PersonalNumber { get; set; }

        [RegulaAttributes(new[] { "fieldName", "Place of Birth", "value" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string PlaceOFBirth { get; set; }

        [RegulaAttributes(new[] { "FieldName", "Place of Issue", "Buf_Text" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string PlaceOfIssue { get; set; }

        [RegulaAttributes(new[] { "FieldName", "Portrait", "image", "image" },
            DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldNameWithSubValue)]
        public string PortraitImage { get; set; }

        [RegulaAttributes(new[] { "FieldName", "Signature", "image", "image" },
            DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldNameWithSubValue)]
        public string Signature { get; set; }

        [RegulaAttributes(new[] { "FieldName", "Surname", "Buf_Text" }, DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)]
        public string Surname { get; set; }

        #region IDsAndDocuments

        public override ScannedDocumentType ScannedDocumentType { get; set; }

        #endregion IDsAndDocuments

        #region Other Properties

        /// <summary>
        /// TODO Make this an extenstion method
        /// </summary>
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
}