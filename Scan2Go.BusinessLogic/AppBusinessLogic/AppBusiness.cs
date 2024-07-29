using Scan2Go.BusinessLogic.BaseClasses;
using Scan2Go.BusinessLogic.MonitoringBusiness;
using Scan2Go.Entity.IdsAndDocuments;
using Utility.Bases;
using Utility.Core;
using System;
using System.Runtime.Caching;
using Scan2Go.Enums.Properties;

namespace Scan2Go.BusinessLogic.AppBusinessLogic
{
    public class AppBusiness : BaseBusiness
    {
        public AppBusiness(OperationResult operationResult, IUser currentUser) : base(operationResult, currentUser)
        {
        }

        public AppBusiness(BaseBusiness baseBusiness) : base(baseBusiness)
        {
        }

        public async Task<IDsAndDocumentsResults> ExtractDataFromDocument(byte[] documentData)
        {
            string documentDataAsString = Convert.ToBase64String(documentData);

            dynamic response = await new MonitoringBusiness.MonitoringBusiness(this).CallRegulaApiAndGetResponse(documentDataAsString);

            IIDsAndDocuments iiDsAndDocuments = new MonitoringLogic().PrepareIdAndDocumentsResultFromResponse(response);

            IDsAndDocumentsResults IDsAndDocumentsResults = new IDsAndDocumentsResults();

            if (iiDsAndDocuments.ScannedDocumentType == Enums.ScannedDocumentType.Id)
            {
                IDsAndDocumentsResults.IdDocuments.Add((IdentityCard)iiDsAndDocuments);
            }
            else if (iiDsAndDocuments.ScannedDocumentType == Enums.ScannedDocumentType.Passport)
            {
                IDsAndDocumentsResults.Passports.Add((Passport)iiDsAndDocuments);
            }
            else if (iiDsAndDocuments.ScannedDocumentType == Enums.ScannedDocumentType.DrivingLicense)
            {
                IDsAndDocumentsResults.DrivingLicenses.Add((DrivingLicense)iiDsAndDocuments);
            }
            else if (iiDsAndDocuments.ScannedDocumentType == Enums.ScannedDocumentType.Visa)
            {
                IDsAndDocumentsResults.Visas.Add((Visa)iiDsAndDocuments);
            }

            //throw new Exception("This is a custom Exception thrown on purpose to test the response stack trace");
            /******************************** Move this to a separate place later ******************************/
            if (IDsAndDocumentsResults.IdDocuments.Any())
            {
                IdentityCard identityCard = IDsAndDocumentsResults.IdDocuments.First();

                identityCard.TransactionId = Guid.NewGuid();

                string key = identityCard.TransactionId.ToString();
                //string data = identityCard.PortraitImage;
                DateTimeOffset expiration = DateTimeOffset.Now.AddMinutes(10);  // Cache for 30 minutes

                CacheHelper.SaveToCache(key, identityCard, expiration);

                //string testing = CacheHelper.GetFromCache(key) as string;
            }
            /***************************************************************************************************/

            return IDsAndDocumentsResults;
        }

        public async Task<bool> CheckFaceMatching(byte[]? documentData, string transactionId)
        {
            string documentDataAsString = Convert.ToBase64String(documentData);
            var identityCard = CacheHelper.GetFromCache(transactionId) as IdentityCard;

            if (identityCard == null)
            {
                this.AddDetailResult(new OperationResult { State = false, MessageStringKey = "No identity card info was found, please restart the operation!" });
                return false;
            }

            string existingImage = identityCard.PortraitImage;

            if (string.IsNullOrEmpty(existingImage))
            {
                this.AddDetailResult(new OperationResult { State = false, MessageStringKey = "No Image was found, please restart the operation!" });
                return false;
            }

            string responseSimilarity = await new MonitoringBusiness.MonitoringBusiness(this)
                .CallRegulaFaceDetectionApiAndGetResponse(existingImage, documentDataAsString);

            if (double.TryParse(responseSimilarity, out double similarityValue))
            {
                if (similarityValue > 0.90000000000)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        public IdentityCard GetIdentityCardFromCache(string transactionId)
        {
            var identityCard = CacheHelper.GetFromCache(transactionId) as IdentityCard;

            if (identityCard == null)
            {
                this.AddDetailResult(new OperationResult { State = false, MessageStringKey = "No identity card info was found, please restart the operation!" });
                return null;
            }

            return identityCard;
        }
    }
    
    
    /// <summary>
    /// TODO Move this to framework later or use the framework one
    /// </summary>
    public class CacheHelper
    {
        private static MemoryCache _cache = new MemoryCache("CachingProvider");

        public static void SaveToCache(string key, object data, DateTimeOffset absoluteExpiration)
        {
            _cache.Add(key, data, absoluteExpiration);
        }

        public static object GetFromCache(string key)
        {
            return _cache.Get(key);
        }
    }

}