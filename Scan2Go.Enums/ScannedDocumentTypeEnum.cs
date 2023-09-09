namespace Scan2Go.Enums;

public enum ScannedDocumentType
{
    Id = 1,
    Passport = 2,
    DrivingLicense = 3,
    Visa = 4
}

public enum DynamicJSonExtractionType
{
    MainFieldNameOnly = 1,
    MainFieldNameWithValueAndSecondFieldName = 2,
    MainFieldNameWithValueAndSecondFieldNameWithSubValue = 3,
    TwoMainFieldNamesWithTwoValuesAndSecondFieldName = 4
}