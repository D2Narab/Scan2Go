namespace Scan2Go.Entity.IdsAndDocuments
{
    public class IDsAndDocumentsResults
    {
        public Cars.Cars Car { get; set; } = new Cars.Cars();
        public IList<DrivingLicense> DrivingLicenses { get; set; } = new List<DrivingLicense>();
        public IList<IdentityCard> IdDocuments { get; set; } = new List<IdentityCard>();
        public IList<Passport> Passports { get; set; } = new List<Passport>();
        public Rents.Rents Rent { get; set; } = new Rents.Rents();
        public IList<Visa> Visas { get; set; } = new List<Visa>();
    }
}