using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Http;
using System.Text;
using Org.BouncyCastle.Asn1.Ocsp;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        string email = "d2demo@outlook.com";
        string password = "d2eco!!!";
        string imapServer = "outlook.office365.com"; // IMAP server address
        int port = 993; // IMAP port
        bool useSsl = true;

        using (var client = new ImapClient())
        {
            client.Connect(imapServer, port, useSsl);
            client.Authenticate(email, password);

            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);

            // Retrieve unread messages from the inbox
            var messages = inbox.Search(SearchQuery.NotSeen);

            foreach (var uid in messages)
            {
                var message = inbox.GetMessage(uid);

                Console.WriteLine($"Subject: {message.Subject}");
                Console.WriteLine($"From: {message.From}");
                Console.WriteLine($"Date: {message.Date}");

                foreach (var attachment in message.Attachments)
                {
                    if (attachment is MimePart mimePart && mimePart.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            mimePart.Content.DecodeTo(memoryStream);

                            byte[] pdfBytes = memoryStream.ToArray();
                            string base64Pdf = Convert.ToBase64String(pdfBytes);

                            // Do something with the base64 PDF data
                            Console.WriteLine("Attachment is a PDF:");
                            Console.WriteLine(base64Pdf);

                            // Specify the folder path and file name
                            string folderPath = @"D:\D2\Scan2Go\MailImports"; // Change this to your desired folder path
                            //string fileName = $"{message.Date.ToString().Replace('/','_')} {mimePart.FileName}.txt";
                            string fileName = "output.txt";

                            // Combine the folder path and file name to create the full file path
                            string filePath = Path.Combine(folderPath, fileName);

                            try
                            {
                                // Write the text to the file
                                File.WriteAllText(filePath, base64Pdf);

                                Console.WriteLine("Text written to file: " + filePath);

                                /******************************************* API Calling ***************************************/
                                // Create the JSON request object
                                var request = new
                                {
                                    processParam = new
                                    {
                                        scenario = "FullProcess"
                                    },
                                    List = new[]
                                    {
                new
                {
                    format = ".jpg",
                    light = 6,
                    page_idx = 0,
                    ImageData = new
                    {
                        image = base64Pdf
                    }
                }
            }
                                };

                                // Serialize the request object to JSON
                                string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(request);

                                // Define the API endpoint URL
                                string apiUrl = "http://localhost:8080/api/process";

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

                                        // You can now work with the responseData as needed
                                        Console.WriteLine("API Response:");
                                        Console.WriteLine(responseData);

                                        // Example: Access a specific property in the response
                                        var someProperty = responseData.List;
                                        Console.WriteLine($"Some Property: {someProperty}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"API Request failed with status code: {response.StatusCode}");
                                    }
                                }

                                /******************************************* API Calling ***************************************/

                                /***************************************** Finding values **************************************/
                                string fieldNameToFind = "Document Number";
                                string bufTextValue = FindBufTextValue(responseData, fieldNameToFind);

                                if (bufTextValue != null)
                                {
                                    Console.WriteLine($"Found FieldName: '{fieldNameToFind}', Buf_Text Value: '{bufTextValue}'");
                                }
                                else
                                {
                                    Console.WriteLine($"Field with FieldName '{fieldNameToFind}' not found.");
                                }

                                // Recursively find the Buf_Text value associated with a specific FieldName
                                static string FindBufTextValue(dynamic data, string fieldName)
                                {
                                    if (data is JObject jsonObject)
                                    {
                                        foreach (var property in jsonObject.Properties())
                                        {
                                            if (property.Name == "FieldName" && property.Value.ToString() == fieldName)
                                            {
                                                // Search for Buf_Text in the same level
                                                var bufTextProperty = jsonObject.Property("Buf_Text");
                                                if (bufTextProperty != null)
                                                {
                                                    return bufTextProperty.Value.ToString();
                                                }
                                            }
                                            else
                                            {
                                                var result = FindBufTextValue(property.Value, fieldName);
                                                if (result != null)
                                                {
                                                    return result;
                                                }
                                            }
                                        }
                                    }
                                    else if (data is JArray jsonArray)
                                    {
                                        foreach (var item in jsonArray)
                                        {
                                            var result = FindBufTextValue(item, fieldName);
                                            if (result != null)
                                            {
                                                return result;
                                            }
                                        }
                                    }

                                    return null;
                                }
                                /***************************************** Finding values **************************************/
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("An error occurred: " + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: Attachment is not a PDF.");
                    }
                }
            }

            client.Disconnect(true);
        }
    }
}
