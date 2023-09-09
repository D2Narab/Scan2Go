using Scan2Go.Entity.BaseClasses;
using Scan2Go.Enums;
using Utility.Extensions;

namespace Scan2Go.Entity.IdsAndDocuments
{
    public class Passport : IIDsAndDocuments
    {
        [RegulaAttributes("fieldName", "Authority", "value")]
        public string Authority { get; set; }

        [RegulaAttributes("FieldName", "Date of Birth", "Buf_Text")]
        public string DateOfBirth { get; set; }

        [RegulaAttributes("FieldMask", "{DAY}{SPACE}{MONTH}{SPACE}{YEAR}", "Buf_Text")]
        public string DateOfExpiry { get; set; }

        [RegulaAttributes("FieldName", "Date of Issue", "Buf_Text")]
        public string DateOfIssue { get; set; }

        public string DocumentCategory { get; set; }

        [RegulaAttributes("fieldName", "Document front side", "valueList", "value")]
        public string DocumentFrontSide { get; set; }

        [RegulaAttributes("DocumentName")]
        public string DocumentName { get; set; }

        [RegulaAttributes("FieldName", "Document Number", "Buf_Text")]
        public string DocumentNumber { get; set; }

        [RegulaAttributes("FieldName", "Ghost portrait", "image", "image")]
        public string GhostPortrait { get; set; }

        [RegulaAttributes("fieldName", "Issuing State Name", "value")]
        public string IssuingStateName { get; set; }

        [RegulaAttributes("FieldName", "Given Names", "Buf_Text")]
        public string Name { get; set; }

        [RegulaAttributes("fieldName", "Nationality", "value")]
        public string Nationality { get; set; }

        [RegulaAttributes("FieldName", "Personal Number", "Buf_Text")]
        public string PersonalNumber { get; set; }

        [RegulaAttributes("fieldName", "Place of Birth", "value")]
        public string PlaceOFBİrth { get; set; }

        [RegulaAttributes("FieldName", "Portrait", "image", "image")]
        public string PortraitImage { get; set; }

        [RegulaAttributes("FieldName", "Signature", "image", "image")]
        public string Signature { get; set; }

        [RegulaAttributes("FieldName", "Surname", "Buf_Text")]
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