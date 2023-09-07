using Scan2Go.Enums;
using Utility.Extensions;

namespace Scan2Go.Entity.IdsAndDocuments
{
    public class IdentityCard : IIDsAndDocuments
    {
        public string DateOfBirth { get; set; }
        public string DateOfExpiry { get; set; }
        public string DocumentFrontSide { get; set; }
        public string DocumentNumber { get; set; }
        public string GhostPortrait { get; set; }
        public string IssuingStateName { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public string PersonalNumber { get; set; }
        public string PortraitImage { get; set; }
        public string ScannedDocumentTypeName => ScannedDocumentType.ToString();
        public string Signature { get; set; }
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