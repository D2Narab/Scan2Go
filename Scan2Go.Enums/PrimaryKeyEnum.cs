using Utility.Core.DataLayer;

namespace Scan2Go.Enums;

public class PrimaryKey : DataLayerEnumBase
{
    public static readonly DataLayerEnumBase DefSchemaId = new PrimaryKey("DefSchemaId");
    public static readonly DataLayerEnumBase TranslationId = new PrimaryKey("TranslationId");
    public static readonly DataLayerEnumBase UserId = new PrimaryKey("UserId");
    public static readonly DataLayerEnumBase CarId = new PrimaryKey("CarId");
    public static readonly DataLayerEnumBase CustomerId = new PrimaryKey("CustomerId");
    public static readonly DataLayerEnumBase RentId = new PrimaryKey("RentId");

    public PrimaryKey(string internalValue) : base(internalValue)
    {
    }

    
}
