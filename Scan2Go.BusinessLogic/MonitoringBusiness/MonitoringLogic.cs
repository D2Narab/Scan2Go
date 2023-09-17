﻿using MimeKit;
using Scan2Go.Entity.BaseClasses;
using Scan2Go.Entity.IdsAndDocuments;
using Scan2Go.Enums;
using System.Reflection;
using MailReader;
using Utility.Extensions;

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
                if (attachment is not MimePart mimePart
                    /*|| mimePart.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) == false*/)
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
            string documentCategory = PrimitiveExtensions.GetFieldValueAccordingToFieldName(container, "dDescription");

            if (string.IsNullOrEmpty(documentCategory))
            {
                continue;
            }

            if (documentCategory.Equals("Identity Card"))
            {
                var identityCard = CreateIdentityCard(container, documentCategory);

                idsAndDocumentsList.Add(identityCard);
            }
            else if (documentCategory.Equals("Passport"))
            {
                var passport = CreatePassport(container, documentCategory);

                idsAndDocumentsList.Add(passport);
            }
            else if (documentCategory.Equals("Driving License"))
            {
                var drivingLicense = CreateDrivingLicense(container, documentCategory);

                idsAndDocumentsList.Add(drivingLicense);
            }
            else if (documentCategory.Equals("Visa"))
            {
                var visa = CreateVisaLicense(container, documentCategory);

                idsAndDocumentsList.Add(visa);
            }
        }

        return idsAndDocumentsList;
    }

    private IIDsAndDocuments CreateVisaLicense(object container, string documentCategory)
    {
        Visa visa = new Visa();

        visa.ScannedDocumentType = ScannedDocumentType.Visa;
        visa.DocumentCategory = documentCategory;

        FillPropertiesAccordingToAttributeValues(container, visa);

        return visa;
    }

    private IIDsAndDocuments CreateDrivingLicense(dynamic container, string documentCategory)
    {
        DrivingLicense drivingLicense = new DrivingLicense();

        drivingLicense.ScannedDocumentType = ScannedDocumentType.DrivingLicense;
        drivingLicense.DocumentCategory = documentCategory;

        FillPropertiesAccordingToAttributeValues(container, drivingLicense);

        return drivingLicense;
    }

    private IIDsAndDocuments CreateIdentityCard(dynamic container, string documentCategory)
    {
        IdentityCard identityCard = new IdentityCard();

        identityCard.ScannedDocumentType = ScannedDocumentType.Id;
        identityCard.DocumentCategory = documentCategory;

        FillPropertiesAccordingToAttributeValues(container, identityCard);

        return identityCard;
    }

    private IIDsAndDocuments CreatePassport(dynamic container, string documentCategory)
    {
        Passport passport = new Passport();

        passport.ScannedDocumentType = ScannedDocumentType.Passport;
        passport.DocumentCategory = documentCategory;

        FillPropertiesAccordingToAttributeValues(container, passport);

        return passport;
    }

    private void FillPropertiesAccordingToAttributeValues(dynamic container, IIDsAndDocuments iDsAndDocuments)
    {
        PropertyInfo[] properties = iDsAndDocuments.GetType().GetProperties();

        foreach (var property in properties)
        {
            if (Attribute.IsDefined(property, typeof(RegulaAttributes)) == false || property.CanWrite == false)
            {
                continue;
            }

            string propertyName = property.Name;

            RegulaAttributes regulaAttributes = null;

            if (iDsAndDocuments is IdentityCard)
            {
                regulaAttributes = typeof(IdentityCard).GetCustomAttribute<RegulaAttributes>(propertyName);
            }
            else if (iDsAndDocuments is Passport)
            {
                regulaAttributes = typeof(Passport).GetCustomAttribute<RegulaAttributes>(propertyName);
            }
            else if (iDsAndDocuments is DrivingLicense)
            {
                regulaAttributes = typeof(DrivingLicense).GetCustomAttribute<RegulaAttributes>(propertyName);
            }
            else if (iDsAndDocuments is Visa)
            {
                regulaAttributes = typeof(Visa).GetCustomAttribute<RegulaAttributes>(propertyName);
            }

            string extractedValue = string.Empty;

            if (regulaAttributes.DynamicJSonExtractionType == DynamicJSonExtractionType.MainFieldNameOnly)
            {
                extractedValue = PrimitiveExtensions.GetFieldValueAccordingToFieldName(container, regulaAttributes.FieldName);
            }
            else if (regulaAttributes.DynamicJSonExtractionType == DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName)
            {
                extractedValue = PrimitiveExtensions.GetFieldValueInTheSameLevelOfAnotherField(container,
                    regulaAttributes.FieldName, regulaAttributes.FieldToBeFoundValue, regulaAttributes.SecondaryFieldName);
            }
            else if (regulaAttributes.DynamicJSonExtractionType == DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldNameWithSubValue)
            {
                extractedValue = PrimitiveExtensions.GetSubFieldValueInTheSameLevelOfAnotherField(container,
                    regulaAttributes.FieldName, regulaAttributes.FieldToBeFoundValue, regulaAttributes.SecondaryFieldName, regulaAttributes.SubFieldName);
            }
            else if (regulaAttributes.DynamicJSonExtractionType == DynamicJSonExtractionType.TwoMainFieldNamesWithTwoValuesAndSecondFieldName)
            {
                extractedValue = PrimitiveExtensions.GetFieldValueInTheSameLevelOfAnotherFields(container,
                    regulaAttributes.FieldName, regulaAttributes.FieldToBeFoundValue,
                    regulaAttributes.SecondMainFieldName, regulaAttributes.SecondMainFieldToBeFoundValue,
                    regulaAttributes.SecondaryFieldName);
            }

            property.SetValue(iDsAndDocuments, extractedValue);
        }
    }
}