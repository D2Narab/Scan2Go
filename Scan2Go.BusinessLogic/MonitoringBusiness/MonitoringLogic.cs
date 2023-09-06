using MailReader;
using MimeKit;
using Org.BouncyCastle.Asn1.Ocsp;
using Scan2Go.Entity.IdsAndDocuments;
using Scan2Go.Enums;

namespace Scan2Go.BusinessLogic.MonitoringBusiness;

internal class MonitoringLogic
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
        //TODO we have to try this with a response that has many, will do it later.
        //foreach (var container in response.ContainerList)
        //{
        string documentType = Utility.Extensions.PrimitiveExtensions
            .GetFieldValueAccordingToFieldName(response.ContainerList, "dDescription");

        if (string.IsNullOrEmpty(documentType) == false && documentType.Equals("Identity Card"))
        {
            IdentityCard identityCard = new IdentityCard();

            identityCard.ScannedDocumentType = ScannedDocumentType.Id;

            string fieldNameToFind = "Personal Number";
            string bufTextValue = Utility.Extensions.PrimitiveExtensions
                .GetFieldValueInTheSameLevelOfAnotherField(response.ContainerList, "FieldName", fieldNameToFind, "Buf_Text");

            identityCard.PersonalNumber = bufTextValue;

            fieldNameToFind = "Given Names";
            bufTextValue = Utility.Extensions.PrimitiveExtensions
                .GetFieldValueInTheSameLevelOfAnotherField(response.ContainerList, "FieldName", fieldNameToFind, "Buf_Text");

            identityCard.Name = bufTextValue;

            fieldNameToFind = "Surname";
            bufTextValue = Utility.Extensions.PrimitiveExtensions
                .GetFieldValueInTheSameLevelOfAnotherField(response.ContainerList, "FieldName", fieldNameToFind, "Buf_Text");

            identityCard.Surname = bufTextValue;

            fieldNameToFind = "Document Number";
            bufTextValue = Utility.Extensions.PrimitiveExtensions
                .GetFieldValueInTheSameLevelOfAnotherField(response.ContainerList, "FieldName", fieldNameToFind, "Buf_Text");

            identityCard.DocumentNumber = bufTextValue;

            idsAndDocumentsList.Add(identityCard);
        }
        //}

        return idsAndDocumentsList;
    }
}