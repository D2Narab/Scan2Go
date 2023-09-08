using MailReader;
using MimeKit;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using Scan2Go.Entity.IdsAndDocuments;
using Scan2Go.Enums;
using System.Data.Common;
using System.Drawing;
using Scan2Go.Entity.BaseClasses;
using Utility.Extensions;
using Scan2Go.Entity.Cars;
using System.Reflection;

namespace Scan2Go.BusinessLogic.MonitoringBusiness;

public class MonitoringLogic
{
    public async Task<IList<string>> ExtractAttachmentsAsBase64(MailReadingResults mailReadingResults)
    {
        IList<string> base64List = new List<string>();

        foreach (var mailId in mailReadingResults.Mails)
        {
            var message = await mailReadingResults.Inbox.GetMessageAsync(mailId);

            foreach (var attachment in message.Attachments)
            {
                if (attachment is not MimePart mimePart ||
                    mimePart.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) == false)
                {
                    //Console.WriteLine("Error: Attachment is not a PDF.");

                    continue;
                }

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await mimePart.Content.DecodeToAsync(memoryStream);

                    byte[] pdfBytes = memoryStream.ToArray();
                    string base64Pdf = Convert.ToBase64String(pdfBytes);

                    base64List.Add(base64Pdf);
                }
            }
        }

        await mailReadingResults.ImapClient.DisconnectAsync(true);

        return base64List;
    }

    public IList<IIDsAndDocuments> PrepareIdAndDocumentsResultFromResponse(dynamic response)
    {
        IList<IIDsAndDocuments> idsAndDocumentsList = new List<IIDsAndDocuments>();

        if (response.ContainerList == null || response.ContainerList.List == null)
        {
            return idsAndDocumentsList;
        }

        foreach (var container in response.ContainerList)
        {
            string documentCategory = Utility.Extensions.PrimitiveExtensions
            .GetFieldValueAccordingToFieldName(container, "dDescription");

            if (string.IsNullOrEmpty(documentCategory) == false && documentCategory.Equals("Identity Card"))
            {
                IdentityCard identityCard = new IdentityCard();

                identityCard.ScannedDocumentType = ScannedDocumentType.Id;
                identityCard.DocumentCategory = documentCategory;

                PropertyInfo[] properties = identityCard.GetType().GetProperties();

                foreach (var property in properties)
                {
                    if (Attribute.IsDefined(property, typeof(RegulaAttributes)) == false || property.CanWrite == false)
                    {
                        continue;
                    }

                    string propertyName = property.Name;

                    var attribute = typeof(IdentityCard).GetCustomAttribute<RegulaAttributes>(propertyName);

                    string extractedValue;

                    if (string.IsNullOrEmpty(attribute.FieldToBeFoundValue))
                    {
                        extractedValue = PrimitiveExtensions.GetFieldValueAccordingToFieldName(container, attribute.FieldName);
                    }
                    else if (string.IsNullOrEmpty(attribute.SubFieldName))
                    {
                        extractedValue = PrimitiveExtensions.GetFieldValueInTheSameLevelOfAnotherField(container,
                            attribute.FieldName, attribute.FieldToBeFoundValue, attribute.SecondaryFieldName);
                    }
                    else
                    {
                        extractedValue = PrimitiveExtensions.GetSubFieldValueInTheSameLevelOfAnotherField(container,
                            attribute.FieldName, attribute.FieldToBeFoundValue, attribute.SecondaryFieldName, attribute.SubFieldName);
                    }

                    property.SetValue(identityCard, extractedValue);
                }

                idsAndDocumentsList.Add(identityCard);
            }
        }

        return idsAndDocumentsList;
    }
}