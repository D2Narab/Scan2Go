using MailKit.Search;
using MailReader;
using Scan2Go.Entity.IdsAndDocuments;
using Scan2Go.Enums.Properties;
using System.Collections.Generic;
using System.Text;
using Scan2Go.BusinessLogic.BaseClasses;
using Scan2Go.BusinessLogic.CarsBusinessLogic;
using Scan2Go.BusinessLogic.CustomersBusinessLogic;
using Scan2Go.BusinessLogic.RentsBusinessLogic;
using Utility.Bases;
using Utility.Core;

namespace Scan2Go.BusinessLogic.MonitoringBusiness;

public class MonitoringBusiness : BaseBusiness
{
    private MonitoringLogic _monitoringLogic;
    private MonitoringLogic MonitoringLogic => _monitoringLogic ??= new MonitoringLogic();

    public MonitoringBusiness(OperationResult operationResult, IUser currentUser) : base(operationResult, currentUser)
    {
    }

    public MonitoringBusiness(BaseBusiness baseBusiness) : base(baseBusiness)
    {
    }

    public async Task<IDsAndDocumentsResults> GetMails()
    {
        MailReadingResults mailReadingResults = await ReadMail();

        IList<string> base64List = await MonitoringLogic.ExtractAttachmentsAsBase64(mailReadingResults);

        //Handle this with a proper message later
        if (base64List.Any() == false)
        {
            return new IDsAndDocumentsResults();
        }

        IList<IIDsAndDocuments> idAndDocumentsList = new List<IIDsAndDocuments>();

        foreach (var base64String in base64List)
        {
            dynamic response = await CallRegulaApiAndGetResponse(base64String);

            IIDsAndDocuments iiDsAndDocuments = MonitoringLogic.PrepareIdAndDocumentsResultFromResponse(response);

            idAndDocumentsList.Add(iiDsAndDocuments);
        }

        IDsAndDocumentsResults IDsAndDocumentsResults = new IDsAndDocumentsResults();
        //TODO Move this later to the constructor of IDsAndDocumentsResults
        foreach (IIDsAndDocuments idAndDocument in idAndDocumentsList)
        {
            if (idAndDocument.ScannedDocumentType == Enums.ScannedDocumentType.Id)
            {
                IDsAndDocumentsResults.IdDocuments.Add((IdentityCard)idAndDocument);
            }
            else if (idAndDocument.ScannedDocumentType == Enums.ScannedDocumentType.Passport)
            {
                IDsAndDocumentsResults.Passports.Add((Passport)idAndDocument);
            }
            else if (idAndDocument.ScannedDocumentType == Enums.ScannedDocumentType.DrivingLicense)
            {
                IDsAndDocumentsResults.DrivingLicenses.Add((DrivingLicense)idAndDocument);
            }
            else if (idAndDocument.ScannedDocumentType == Enums.ScannedDocumentType.Visa)
            {
                IDsAndDocumentsResults.Visas.Add((Visa)idAndDocument);
            }
        }

        /************************************************ Analyze,extract and check ****************************************/
        string? customerName = IDsAndDocumentsResults.IdDocuments.FirstOrDefault()?.FullName;

        if (string.IsNullOrEmpty(customerName))
        {
            customerName = IDsAndDocumentsResults.Passports.FirstOrDefault()?.FullName;
        }

        if (string.IsNullOrEmpty(customerName))
        {
            customerName = IDsAndDocumentsResults.Visas.FirstOrDefault()?.FullName;
        }

        if (string.IsNullOrEmpty(customerName))
        {
            customerName = IDsAndDocumentsResults.DrivingLicenses.FirstOrDefault()?.FullName;
        }

        /*TODO Move this to validation later*/
        if (string.IsNullOrEmpty(customerName))
        {
            this.AddDetailResult(new OperationResult { State = false, MessageStringKey = nameof(MessageStrings.WasNotAbleToExtractFullName) });

            return new IDsAndDocumentsResults();
        }

        var rent = new RentsBusiness(this).GetRentByCustomerName(customerName);
        
        
        /*TODO Move this to validation later*/
        if (rent is null)
        {
            //this.AddDetailResult(new OperationResult { State = false, MessageStringKey = nameof(MessageStrings.NoRentWasFoundWithTheFullName) });

            foreach (IIDsAndDocuments idAndDocument in idAndDocumentsList)
            {
                idAndDocument.ErrorMessages.Add(new string("Rent was not found for the supplied customer!"));
            }

            return IDsAndDocumentsResults;
        }

        var car = new CarsBusiness(this).GetCars(rent.CarId);
        var customer = new CustomersBusiness(this).GetCustomers(rent.CustomerId);
        
        MonitoringLogic.CheckAllDocumentsForValidation(IDsAndDocumentsResults, rent, customer);

        /*******************************************************************************************************************/

        IDsAndDocumentsResults.Rent = rent;
        IDsAndDocumentsResults.Car = car;
        
        return IDsAndDocumentsResults;
    }

    /// <summary>
    /// This will be moved to a separate class later.
    /// </summary>
    /// <returns></returns>
    private async Task<dynamic> CallRegulaApiAndGetResponse(string base64String)
    {
        var listElements = new List<object>();

        // Create an object for each element in the "List" variable
        var listElement = new
        {
            format = ".jpg",
            light = 6,
            page_idx = 0,
            ImageData = new
            {
                image = base64String
            }
        };

        // Add the object to the list
        listElements.Add(listElement);


        // Create the final request object with the list
        var request = new
        {
            processParam = new
            {
                scenario = "FullProcess"
            },
            List = listElements.ToArray() // Convert the list to an array
        };

        // Serialize the request object to JSON
        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(request);

        // Define the API endpoint URL
        string apiUrl = "http://NASER-LNV:8080/api/process";

        dynamic responseData = null;

        using (var httpClient = new HttpClient())
        {
            // Create a StringContent with the JSON data
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            // Send the POST request
            HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a dynamic object
                responseData = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                Console.WriteLine($"API Request failed with status code: {response.StatusCode}");
            }
        }

        return responseData;
    }

    private async Task<MailReadingResults> ReadMail()
    {
        MailReadingResults mailReadingResults = await MailReader.MailReader.ReadMail(new MailReadingProperties
        {
            EmailAddress = "d2demo@outlook.com",
            Password = "d2eco!!!",
            ImapServer = "outlook.office365.com",
            Port = 993,
            UseSsl = true,
            SearchQuery = SearchQuery.NotSeen,
            DisconnectClient = false
        });

        return mailReadingResults;
    }
}