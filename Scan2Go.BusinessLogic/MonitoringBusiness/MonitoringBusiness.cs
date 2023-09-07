using MailKit.Search;
using MailReader;
using Scan2Go.Entity.IdsAndDocuments;
using System.Text;

namespace Scan2Go.BusinessLogic.MonitoringBusiness;

public class MonitoringBusiness
{
    private MonitoringLogic _monitoringLogic;
    private MonitoringLogic MonitoringLogic => _monitoringLogic ??= new MonitoringLogic();

    public async Task<IList<IIDsAndDocuments>> GetMails()
    {
        MailReadingResults mailReadingResults = await ReadMail();

        IList<string> base64List = await MonitoringLogic.ExtractAttachmentsAsBase64(mailReadingResults);

        dynamic response = await CallRegulaApiAndGetResponse(base64List);

        IList<IIDsAndDocuments> idAndDocumentsList = MonitoringLogic.PrepareIdAndDocumentsResultFromResponse(response);

        return idAndDocumentsList;
    }

    /// <summary>
    /// This will be moved to a separate class later.
    /// </summary>
    /// <returns></returns>
    private async Task<dynamic> CallRegulaApiAndGetResponse(IList<string> base64List)
    {
        var listElements = new List<object>();

        foreach (var base64String in base64List)
        {
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
        }

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