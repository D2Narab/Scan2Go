using Scan2Go.Enums;

namespace Scan2Go.Entity.IdsAndDocuments
{
    public class IdentityCard : IIDsAndDocuments
    {
        public string DateOfExpiry { get; set; }
        public string DocumentFrontSide { get; set; }
        public string DocumentNumber { get; set; }
        public string Name { get; set; }
        public string PersonalNumber { get; set; }
        public string PortraitImage { get; set; }
        public string ScannedDocumentTypeName => ScannedDocumentType.ToString();
        public string Surname { get; set; }

        #region IDsAndDocuments

        public override ScannedDocumentType ScannedDocumentType { get; set; }

        #endregion IDsAndDocuments
    }
}