using Scan2Go.Enums;

namespace Scan2Go.Entity.IdsAndDocuments
{
    public class IdentityCard : IDsAndDocuments
    {
        public string DocumentNumber { get; set; }
        public string Name { get; set; }
        public string PersonalNumber { get; set; }
        public string Surname { get; set; }

        #region IDsAndDocuments

        public ScannedDocumentType ScannedDocumentType { get; set; }

        #endregion IDsAndDocuments
    }
}