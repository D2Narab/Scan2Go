using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scan2Go.Api.BaseClasses;
using Scan2Go.Enums;
using System.Net;
using Newtonsoft.Json;
using Scan2Go.Mapper.Managers;
using Utility.Core;
using Utility.Extensions;
using System.Xml.Linq;
using Scan2Go.Mapper.Models.CustomersModels;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Path = System.IO.Path;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;
using DocumentFormat.OpenXml;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using System.Net.Mail;
using Body = DocumentFormat.OpenXml.Wordprocessing.Body;
using Microsoft.Office.Interop.Word;
using MailMessage = System.Net.Mail.MailMessage;
using Task = System.Threading.Tasks.Task;
using System.Transactions;
using Scan2Go.BusinessLogic.AppBusinessLogic;
using Scan2Go.Entity.Customers;
using Scan2Go.Entity.IdsAndDocuments;

namespace Scan2Go.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AppController : BaseController
{
    private readonly ILogger<AppController> _logger;

    public AppController(ILogger<AppController> logger)
    {
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("CheckConnection")]
    public IActionResult CheckConnection()
    {
        OperationResult operationResult = new OperationResult();
        operationResult.ResultObject = true;

        return Ok(operationResult);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("UploadIdDocument")]
    public async Task<IActionResult> UploadIdDocument()
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        string logMessge = $"UploadIdDocument was called at {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()} ";
        logMessge += $"From IP {ipAddress}";

        _logger.LogInformation(logMessge);

        var httpRequest = HttpContext.Request;
        OperationResult operationResult = new OperationResult();

        if (httpRequest is null)
        {
            _logger.LogInformation("httpRequest is null! ");

            return StatusCode(HttpStatusCode.ExpectationFailed.AsInt(), operationResult);
        }

        if (httpRequest.Form is null)
        {
            _logger.LogInformation("httpRequest.Form is null! ");

            return StatusCode(HttpStatusCode.ExpectationFailed.AsInt(), operationResult);
        }

        if (httpRequest.Form.Files is null)
        {
            _logger.LogInformation("httpRequest.Form.Files is null! ");

            return StatusCode(HttpStatusCode.ExpectationFailed.AsInt(), operationResult);
        }

        if (httpRequest.Form.Files.Count <= 0)
        {
            operationResult.Message = "No files were found!";
            operationResult.State = false;
            return StatusCode(HttpStatusCode.ExpectationFailed.AsInt(), operationResult);
        }

        //TODO lets make a control that only one file could be sent.

        byte[] documentData = null;

        foreach (IFormFile file in httpRequest.Form.Files)
        {
            _logger.LogInformation("Entered foreach");
            var postedFile = file;

            if (postedFile.Length == 0)
            {
                _logger.LogInformation("Broke out of foreach since posted file Length is 0 ");
                continue;
            }

            using (BinaryReader reader = new BinaryReader(postedFile.OpenReadStream()))
            {
                documentData = reader.ReadBytes(postedFile.Length.AsInt());
            }

            _logger.LogInformation($"Byte[] size is: {documentData.Length.ToString()}");
        }

        operationResult = await new AppManager(this.CurrentUser).UploadIdDocument(documentData);

        var jsonString = JsonConvert.SerializeObject(operationResult.ResultObject);
        _logger.LogInformation($"Returning response as: {jsonString}");

        return this.ReturnOperationResult(operationResult);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("CheckFaceMatching/{transactionId}")]
    public async Task<IActionResult> CheckFaceMatching(string transactionId)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        string logMessge = $"CheckFaceMatching was called at {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()} ";
        logMessge += $"From IP {ipAddress}";
        _logger.LogInformation(logMessge);

        if (string.IsNullOrEmpty(transactionId))
        {
            OperationResult errorOperationResult = new OperationResult();
            errorOperationResult.Message = "transactionId is null or empty!";
            errorOperationResult.State = false;
            return StatusCode(HttpStatusCode.ExpectationFailed.AsInt(), errorOperationResult);
        }

        //var sentTransactionId = transactionId;

        var httpRequest = HttpContext.Request;
        OperationResult operationResult = new OperationResult();

        if (httpRequest.Form.Files.Count <= 0)
        {
            operationResult.Message = "No files were found!";
            operationResult.State = false;
            return StatusCode(HttpStatusCode.ExpectationFailed.AsInt(), operationResult);
        }

        //TODO lets make a control that only one file could be sent.

        byte[] documentData = null;

        foreach (IFormFile file in httpRequest.Form.Files)
        {
            _logger.LogInformation("Entered foreach");
            var postedFile = file;

            if (postedFile.Length == 0)
            {
                _logger.LogInformation("Broke out of foreach since posted file Length is 0 ");
                continue;
            }

            using (BinaryReader reader = new BinaryReader(postedFile.OpenReadStream()))
            {
                documentData = reader.ReadBytes(postedFile.Length.AsInt());
            }

            _logger.LogInformation($"Byte[] size is: {documentData.Length.ToString()}");
        }

        operationResult = await new AppManager(this.CurrentUser).CheckFaceMatching(documentData, transactionId);

        var jsonString = JsonConvert.SerializeObject(operationResult.ResultObject);
        _logger.LogInformation($"Returning response as: {jsonString}");

        return this.ReturnOperationResult(operationResult);
    }
    
    #region Proof Document Operations

    [AllowAnonymous]
    [HttpPost]
    [Route("UploadProofDocument/{transactionId}")]
    public async Task<IActionResult> UploadProofDocument(string transactionId)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var logMessage = $"UploadProofDocument was called at {DateTime.Now:G} from IP {ipAddress}";
        _logger.LogInformation(logMessage);

        var httpRequest = HttpContext.Request;
        var operationResult = new OperationResult();

        if (httpRequest.Form.Files.Count == 0)
        {
            operationResult.Message = "No files were found!";
            operationResult.State = false;
            return StatusCode(StatusCodes.Status417ExpectationFailed, operationResult);
        }

        const string importFolderPath = @"C:\ImportFolder\D2-Dubai Demo";
        const string exportFolderPath = @"C:\ExportFolder\D2-Dubai Demo";

        try
        {
            foreach (var file in httpRequest.Form.Files)
            {
                if (file.Length == 0)
                {
                    _logger.LogWarning("Empty file received, skipping.");
                    continue;
                }

                var filePath = Path.Combine(importFolderPath, file.FileName);

                // Ensure the target directory exists
                if (!Directory.Exists(importFolderPath))
                {
                    Directory.CreateDirectory(importFolderPath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                _logger.LogInformation($"File {file.FileName} saved to {filePath}");
            }

            // Start monitoring the export folder
            var (monitorResult, extractedData) = await MonitorExportFolderAsync(exportFolderPath, _logger, TimeSpan.FromSeconds(60));

            if (extractedData is null || extractedData.Any() == false)
            {
                operationResult.Message = "Monitoring timed out without finding XML files.";
                operationResult.State = false;
                return StatusCode(StatusCodes.Status408RequestTimeout, operationResult);
            }

            operationResult.Message = "Files successfully uploaded and processed.";
            operationResult.State = true;

            CustomersModel customersModel = new CustomersModel();

            customersModel.CustomerName = extractedData["GivenName"];
            customersModel.CustomerSurname = extractedData["SurName"];
            customersModel.HomeAdress = extractedData["Address"];

            CacheHelper.SaveToCache(transactionId+"CustomerObject", customersModel, DateTimeOffset.Now.AddMinutes(10));

            operationResult.ResultObject = customersModel;

            // Delete all files after processing
            var allFiles = Directory.GetFiles(exportFolderPath);
            foreach (var file in allFiles)
            {
                System.IO.File.Delete(file);
            }

            _logger.LogInformation("Processed and deleted all files in the folder.");

            return Ok(operationResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while uploading files.");
            operationResult.Message = "An error occurred while uploading files.";
            operationResult.State = false;
            return StatusCode(StatusCodes.Status500InternalServerError, operationResult);
        }
    }

    private async Task<(bool, Dictionary<string, string>)> MonitorExportFolderAsync(string exportFolderPath, ILogger logger, TimeSpan timeout)
    {
        using (var cts = new CancellationTokenSource(timeout))
        {
            try
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    var xmlFiles = Directory.GetFiles(exportFolderPath, "*.xml").ToList();

                    if (xmlFiles.Count >= 4)
                    {
                        logger.LogInformation("Found 4 or more XML files. Processing...");

                        var extractedData = new Dictionary<string, string>();

                        foreach (var file in xmlFiles)
                        {
                            var doc = XDocument.Load(file);
                            ExtractData(doc, extractedData);
                        }

                        return (true, extractedData);
                    }

                    await Task.Delay(1000, cts.Token); // Check every second
                }

                logger.LogWarning("Monitoring timed out without finding 4 XML files.");
                return (false, null);
            }
            catch (TaskCanceledException)
            {
                logger.LogWarning("Monitoring was canceled due to timeout.");
                return (false, null);
            }
        }
    }

    private void ExtractData(XDocument doc, Dictionary<string, string> extractedData)
    {
        var stepResults = doc.Descendants("StepResult");

        foreach (var step in stepResults)
        {
            var type = step.Element("Type")?.Value;
            var value = step.Element("Value")?.Value;
            var outputField = step.Element("OutputField")?.Value;

            if (type == "Field")
            {
                if (outputField == "GivenName" || outputField == "SurName" || outputField == "Address")
                {
                    extractedData[outputField] = value;
                }
            }
            else if (type == "Label")
            {
                var nestedSteps = step.Descendants("StepResult");
                foreach (var nestedStep in nestedSteps)
                {
                    var nestedType = nestedStep.Element("Type")?.Value;
                    var nestedValue = nestedStep.Element("Value")?.Value;
                    var nestedOutputField = nestedStep.Element("OutputField")?.Value;

                    if (nestedType == "Field")
                    {
                        if (nestedOutputField == "GivenName" || nestedOutputField == "SurName")
                        {
                            extractedData[nestedOutputField] = nestedValue;
                        }
                    }
                }
            }
        }
    }

    #endregion Proof Document Operations

    [AllowAnonymous]
    [HttpPost]
    [Route("UploadSignature/{eMail}/{transactionId}")]
    public async Task<IActionResult> UploadSignature(string eMail, string transactionId)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        string logMessge = $"UploadSignature was called at {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()} ";
        logMessge += $"From IP {ipAddress}";

        _logger.LogInformation(logMessge);

        var httpRequest = HttpContext.Request;
        OperationResult operationResult = new OperationResult();

        if (httpRequest.Form.Files.Count <= 0)
        {
            operationResult.Message = "No files were found!";
            operationResult.State = false;
            return StatusCode(HttpStatusCode.ExpectationFailed.AsInt(), operationResult);
        }

        //TODO lets make a control that only one file could be sent.

        byte[] signatureImageBytes = null;
        bool breakOperation = false;

        foreach (IFormFile file in httpRequest.Form.Files)
        {
            _logger.LogInformation("Entered foreach");
            var postedFile = file;

            if (postedFile.Length == 0)
            {
                _logger.LogInformation("Broke out of foreach since posted file Length is 0 ");
                breakOperation = true;
                break;
            }

            using (BinaryReader reader = new BinaryReader(postedFile.OpenReadStream()))
            {
                signatureImageBytes = reader.ReadBytes(postedFile.Length.AsInt());
            }

            _logger.LogInformation($"Byte[] size is: {signatureImageBytes.Length.ToString()}");

            /************************************ Replacing doc vars in the word doc ***********************************/
            IdentityCard identityCard = new AppManager(this.CurrentUser).GetIdentityCard(transactionId);

            var customers = CacheHelper.GetFromCache(transactionId+ "CustomerObject") as CustomersModel;

            if (customers != null)
            {
                customers.EMail = eMail;
            }
            else
            {
                customers = new CustomersModel();
            }

           

            string filePath = @"D:\D2\Projects\ACCOUNT OPENING FORM DEMO 2.docx";
            string newFilePath = @"D:\D2\Projects\ACCOUNT OPENING FORM DEMO filled.docx";
            string newPdfPath = @"D:\D2\Projects\ACCOUNT OPENING FORM DEMO filled.pdf";

            ReplaceVariablesInWordDocument(filePath, newFilePath, identityCard, signatureImageBytes, customers);
            //ConvertWordToPdf(newFilePath, newPdfPath);
            //SendEmailWithAttachment(newPdfPath, eMail);
        }

        if (breakOperation == false)
        {
            operationResult.ResultObject = true;
        }
        else
        {
            operationResult.ResultObject = false;
        }

        return Ok(operationResult);
    }

    private static void ConvertWordToPdf(string wordFilePath, string pdfFilePath)
    {
        Application wordApp = new Application();
        Microsoft.Office.Interop.Word.Document wordDoc = null;

        try
        {
            wordDoc = wordApp.Documents.Open(wordFilePath);
            wordDoc.ExportAsFixedFormat(pdfFilePath, WdExportFormat.wdExportFormatPDF);
        }
        finally
        {
            if (wordDoc != null)
            {
                wordDoc.Close(WdSaveOptions.wdDoNotSaveChanges);
                wordDoc = null;
            }

            if (wordApp != null)
            {
                wordApp.Quit();
                wordApp = null;
            }
        }
    }

    private static void SendEmailWithAttachment(string filePath, string recipientEmail)
    {
        string smtpServer = "outlook.office365.com"; // Replace with your SMTP server
        int smtpPort = 587; // Replace with your SMTP port
        string smtpUser = "d2demo@outlook.com"; // Replace with your email
        string smtpPass = "d2eco!!!"; // Replace with your email password

        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(smtpUser);
        mail.To.Add(recipientEmail);
        mail.Subject = "Customer onboarding agreement";
        mail.Body = "Here is the agreement";
        mail.Attachments.Add(new Attachment(filePath));

        SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort)
        {
            Credentials = new NetworkCredential(smtpUser, smtpPass),
            EnableSsl = true
        };

        smtpClient.Send(mail);
    }

    private static void ReplaceVariablesInWordDocument(string originalFilePath, string newFilePath, IdentityCard identityCard,
        byte[] customerSignatureImage,CustomersModel customersModel)
    {
        // Copy the original file to a new file
        System.IO.File.Copy(originalFilePath, newFilePath, true);

        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(newFilePath, true))
        {
            Body body = wordDoc.MainDocumentPart.Document.Body;

            // Replace text variables
            foreach (var text in body.Descendants<Text>())
            {
                if (text.Text.Trim().Equals("@CustomerName@"))
                {
                    text.Text = text.Text.Replace("@CustomerName@", identityCard.Name);
                }
                else if (text.Text.Trim().Equals("@CustomerSurname@"))
                {
                    text.Text = text.Text.Replace("@CustomerSurname@", identityCard.Surname);
                }
                else if (text.Text.Trim().Equals("@CustomerName@ @CustomerSurname@"))
                {
                    text.Text = text.Text.Replace("@CustomerName@ @CustomerSurname@", identityCard.Name + " " + identityCard.Surname);
                }
                else if (text.Text.Trim().Equals("@CustomerBirthdate@"))
                {
                    text.Text = text.Text.Replace("@CustomerBirthdate@", identityCard.DateOfBirth);
                }
                else if (text.Text.Trim().Equals("@DocumentId@") || text.Text.Trim().Equals(": @DocumentId@"))
                {
                    text.Text = text.Text.Replace("@DocumentId@",
                        Guid.NewGuid().ToString().Replace("-", string.Empty) + Guid.NewGuid().ToString().Replace("-", string.Empty));
                }
                else if (text.Text.Trim().Equals("@CustomerEMail@"))
                {
                    text.Text = text.Text.Replace("@CustomerEMail@", customersModel.EMail);
                }
                else if (text.Text.Trim().Equals("@HomeAdress@"))
                {
                    text.Text = text.Text.Replace("@HomeAdress@", customersModel.HomeAdress);
                }
                else if (text.Text.Trim().Equals("@DateTime@"))
                {
                    text.Text = text.Text.Replace("@DateTime@", DateTime.Now.ToShortDateString());
                }
            }

            // Replace image variables
            foreach (var paragraph in body.Descendants<DocumentFormat.OpenXml.Wordprocessing.Paragraph>())
            {
                if (paragraph.InnerText.Contains("@CustomerSignature"))
                {
                    foreach (var run in paragraph.Descendants<DocumentFormat.OpenXml.Wordprocessing.Run>())
                    {
                        foreach (var text in run.Descendants<Text>())
                        {
                            if (text.Text.Trim().Equals("@CustomerSignature@"))
                            {
                                text.Text = text.Text.Replace("@CustomerSignature@", string.Empty);
                                InsertImage(paragraph, wordDoc.MainDocumentPart, customerSignatureImage);
                            }
                            else if (text.Text.Trim().Equals("@CustomerSignatureSecond@"))
                            {
                                text.Text = text.Text.Replace("@CustomerSignatureSecond@", string.Empty);
                                InsertImage(paragraph, wordDoc.MainDocumentPart, customerSignatureImage);
                            }
                        }
                    }
                }
            }

            wordDoc.MainDocumentPart.Document.Save();
        }
    }

    private static void InsertImage(DocumentFormat.OpenXml.Wordprocessing.Paragraph paragraph, MainDocumentPart mainPart, byte[] imageBytes)
    {
        string imagePartId;

        using (MemoryStream ms = new MemoryStream(imageBytes))
        {
            ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Png);
            imagePart.FeedData(ms);
            imagePartId = mainPart.GetIdOfPart(imagePart);
        }

        var element = new Drawing(
            new DW.Inline(
                new DW.Extent() { Cx = 990000L, Cy = 792000L },
                new DW.EffectExtent()
                {
                    LeftEdge = 0L,
                    TopEdge = 0L,
                    RightEdge = 0L,
                    BottomEdge = 0L
                },
                new DW.DocProperties()
                {
                    Id = (UInt32Value)1U,
                    Name = "Picture 1"
                },
                new DW.NonVisualGraphicFrameDrawingProperties(
                    new A.GraphicFrameLocks() { NoChangeAspect = true }),
                new A.Graphic(
                    new A.GraphicData(
                        new PIC.Picture(
                            new PIC.NonVisualPictureProperties(
                                new PIC.NonVisualDrawingProperties()
                                {
                                    Id = (UInt32Value)0U,
                                    Name = "New Bitmap Image.png"
                                },
                                new PIC.NonVisualPictureDrawingProperties()),
                            new PIC.BlipFill(
                                new A.Blip(
                                    new A.BlipExtensionList(
                                        new A.BlipExtension()
                                        {
                                            Uri =
                                            "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                        })
                                )
                                {
                                    Embed = imagePartId,
                                    CompressionState =
                                    A.BlipCompressionValues.Print
                                },
                                new A.Stretch(
                                    new A.FillRectangle())),
                            new PIC.ShapeProperties(
                                new A.Transform2D(
                                    new A.Offset() { X = 0L, Y = 0L },
                                    new A.Extents() { Cx = 990000L, Cy = 792000L }),
                                new A.PresetGeometry(
                                    new A.AdjustValueList()
                                )
                                { Preset = A.ShapeTypeValues.Rectangle }))
                    )
                    { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" }))
            {
                DistanceFromTop = (UInt32Value)0U,
                DistanceFromBottom = (UInt32Value)0U,
                DistanceFromLeft = (UInt32Value)0U,
                DistanceFromRight = (UInt32Value)0U,
                EditId = "50D07946"
            });

        paragraph.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Run(element));
    }
}