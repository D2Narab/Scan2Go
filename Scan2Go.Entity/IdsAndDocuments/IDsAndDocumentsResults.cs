namespace Scan2Go.Entity.IdsAndDocuments
{
    public class IDsAndDocumentsResults
    {
        public IList<DrivingLicense> DrivingLicenses { get; set; } = new List<DrivingLicense>();
        public IList<IdentityCard> IdDocuments { get; set; } = new List<IdentityCard>();
        public IList<Passport> Passports { get; set; } = new List<Passport>();
    }
}