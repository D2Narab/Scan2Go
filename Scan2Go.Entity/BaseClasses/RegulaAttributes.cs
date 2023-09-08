namespace Scan2Go.Entity.BaseClasses
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RegulaAttributes : Attribute
    {
        public RegulaAttributes(string fieldName)
        {
            FieldName = fieldName;
            FieldToBeFoundValue = string.Empty;
            SecondaryFieldName = string.Empty;
            SubFieldName = string.Empty;
        }

        public RegulaAttributes(string fieldName, string fieldToBeFoundValue, string secondaryFieldName)
        {
            FieldName = fieldName;
            FieldToBeFoundValue = fieldToBeFoundValue;
            SecondaryFieldName = secondaryFieldName;
            SubFieldName = string.Empty;
        }

        public RegulaAttributes(string fieldName, string fieldToBeFoundValue, string secondaryFieldName, string subFieldName)
        {
            FieldName = fieldName;
            FieldToBeFoundValue = fieldToBeFoundValue;
            SecondaryFieldName = secondaryFieldName;
            SubFieldName = subFieldName;
        }

        public string FieldName { get; set; }
        public string FieldToBeFoundValue { get; set; }
        public string SecondaryFieldName { get; set; }
        public string SubFieldName { get; set; }
    }
}