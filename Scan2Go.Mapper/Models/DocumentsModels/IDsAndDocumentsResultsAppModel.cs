using Scan2Go.Entity.IdsAndDocuments;

namespace Scan2Go.Mapper.Models.DocumentsModels
{
    public class IDsAndDocumentsResultsAppModel
    {
        public IList<DrivingLicense> DrivingLicenses { get; set; } = new List<DrivingLicense>();
        public IList<IdentityCardAppModel> IdDocuments { get; set; } = new List<IdentityCardAppModel>();
        public IList<Passport> Passports { get; set; } = new List<Passport>();
    }
}