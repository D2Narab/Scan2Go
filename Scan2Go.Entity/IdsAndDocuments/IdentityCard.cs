using Scan2Go.Enums;
using Utility.Extensions;

namespace Scan2Go.Entity.IdsAndDocuments
{
    public class IdentityCard : IIDsAndDocuments
    {
        public string DocumentNumber { get; set; }
        public string Name { get; set; }
        public string PersonalNumber { get; set; }
        public string Surname { get; set; }

        public string ScannedDocumentTypeName => ScannedDocumentType.ToString();

        #region IDsAndDocuments

        public override ScannedDocumentType ScannedDocumentType { get; set; }

        #endregion IDsAndDocuments

        public override int RowId => PersonalNumber.AsInt();
    }
}