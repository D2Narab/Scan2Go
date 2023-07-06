using Utility.Core.DataLayer;

namespace Scan2Go.Enums
{
    public class PrimaryKey : DataLayerEnumBase
    {
        public static readonly DataLayerEnumBase DefSchemaId = new PrimaryKey("DefSchemaId");
        public static readonly DataLayerEnumBase TranslationId = new PrimaryKey("TranslationId");

        public PrimaryKey(string internalValue) : base(internalValue)
        {
        }
    }
}