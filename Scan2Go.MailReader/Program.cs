using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System;
using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Net.Http;
using System.Text;
using MailReader;
using Org.BouncyCastle.Asn1.Ocsp;
using Newtonsoft.Json.Linq;
using Utility;

class Program
{
    static async Task Main(string[] args)
    {
        //string email = "d2demo@outlook.com";
        //string password = "d2eco!!!";
        //string imapServer = "outlook.office365.com"; // IMAP server address
        //int port = 993; // IMAP port
        //bool useSsl = true;

        //IList<MailKit.UniqueId> mails;
        //IMailFolder inbox;

        //using (var client = new ImapClient())
        //{
        //    await client.ConnectAsync(imapServer, port, useSsl);
        //    await client.AuthenticateAsync(email, password);

        //    inbox = client.Inbox;
        //    await inbox.OpenAsync(FolderAccess.ReadOnly);

        //    // Retrieve unread messages from the inbox
        //    mails = await inbox.SearchAsync(SearchQuery.NotSeen);
        //    await client.DisconnectAsync(true);
        //}

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

        foreach (var mailId in mailReadingResults.Mails)
        {
            var message = await mailReadingResults.Inbox.GetMessageAsync(mailId);

            Console.WriteLine($"Subject: {message.Subject}");
            Console.WriteLine($"From: {message.From}");
            Console.WriteLine($"Date: {message.Date}");

            foreach (var attachment in message.Attachments)
            {
                if (attachment is not MimePart mimePart ||
                    mimePart.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) == false)
                {
                    Console.WriteLine("Error: Attachment is not a PDF.");

                    continue;
                }

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await mimePart.Content.DecodeToAsync(memoryStream);

                    byte[] pdfBytes = memoryStream.ToArray();
                    string base64Pdf = Convert.ToBase64String(pdfBytes);

                    // Do something with the base64 PDF data
                    //Console.WriteLine("Attachment is a PDF:");
                    //Console.WriteLine(base64Pdf);

                    // Specify the folder path and file name
                    //string folderPath = @"D:\D2\Scan2Go\MailImports"; // Change this to your desired folder path
                    //string fileName = $"{message.Date.ToString().Replace('/','_')} {mimePart.FileName}.txt";
                    //string fileName = "output.txt";

                    // Combine the folder path and file name to create the full file path
                    //string filePath = Path.Combine(folderPath, fileName);

                    try
                    {
                        // Write the text to the file
                        //File.WriteAllText(filePath, base64Pdf);

                        //Console.WriteLine("Text written to file: " + filePath);

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

                                // You can now work with the responseData as needed
                                //Console.WriteLine("API Response:");
                                //Console.WriteLine(responseData);

                                // Example: Access a specific property in the response
                                //var someProperty = responseData.List;
                                //Console.WriteLine($"Some Property: {someProperty}");
                            }
                            else
                            {
                                Console.WriteLine($"API Request failed with status code: {response.StatusCode}");
                            }
                        }

                        /******************************************* API Calling ***************************************/

                        /***************************************** Finding values **************************************/
                        string fieldNameToFind = "Personal Number";
                        //string bufTextValue = FindBufTextValue(responseData, fieldNameToFind);
                        string bufTextValue = Utility.Extensions.PrimitiveExtensions
                            .GetFieldValueInTheSameLevelOfAnotherField(responseData, "FieldName", fieldNameToFind, "Buf_Text");

                        if (bufTextValue != null)
                        {
                            Console.WriteLine($"Found FieldName: '{fieldNameToFind}', Buf_Text Value: '{bufTextValue}'");
                        }
                        else
                        {
                            Console.WriteLine($"Field with FieldName '{fieldNameToFind}' not found.");
                        }

                        /***************************************** Finding values **************************************/
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }
                }
            }
        }

        await mailReadingResults.ImapClient.DisconnectAsync(true);
    }
}
