using Scan2Go.Enums;
using Utility.Bases.EntityBases;

namespace Scan2Go.Entity.IdsAndDocuments;

public abstract class IIDsAndDocuments
{
    public abstract ScannedDocumentType ScannedDocumentType { get; set; }
}