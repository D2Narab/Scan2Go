using Scan2Go.Enums;

namespace Scan2Go.Entity.BaseClasses
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RegulaAttributes : Attribute
    {
        public RegulaAttributes(string[] attributeValues, DynamicJSonExtractionType dynamicJSonExtractionType)
        {
            DynamicJSonExtractionType = dynamicJSonExtractionType;

            switch (dynamicJSonExtractionType)
            {
                case DynamicJSonExtractionType.MainFieldNameOnly:
                    FieldName = attributeValues[0];
                    break;

                case DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldName:
                    FieldName = attributeValues[0];
                    FieldToBeFoundValue = attributeValues[1];
                    SecondaryFieldName = attributeValues[2];
                    break;

                case DynamicJSonExtractionType.MainFieldNameWithValueAndSecondFieldNameWithSubValue:
                    FieldName = attributeValues[0];
                    FieldToBeFoundValue = attributeValues[1];
                    SecondaryFieldName = attributeValues[2];
                    SubFieldName = attributeValues[3];
                    break;

                case DynamicJSonExtractionType.TwoMainFieldNamesWithTwoValuesAndSecondFieldName:
                    FieldName = attributeValues[0];
                    FieldToBeFoundValue = attributeValues[1];
                    SecondMainFieldName = attributeValues[2];
                    SecondMainFieldToBeFoundValue = attributeValues[3];
                    SecondaryFieldName = attributeValues[4];
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(dynamicJSonExtractionType), dynamicJSonExtractionType, null);
            }
        }

        public string FieldName { get; set; }
        public string FieldToBeFoundValue { get; set; } = string.Empty;
        public string SecondMainFieldName { get; set; } = string.Empty;
        public string SecondMainFieldToBeFoundValue { get; set; } = string.Empty;
        public string SecondaryFieldName { get; set; } = string.Empty;
        public string SubFieldName { get; set; } = string.Empty;
        public DynamicJSonExtractionType DynamicJSonExtractionType { get; set; }
    }
}