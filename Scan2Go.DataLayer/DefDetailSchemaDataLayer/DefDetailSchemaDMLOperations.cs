using DataLayer.Base.DatabaseFactory;
using DataLayer.Base.GeneralDataLayer;
using Scan2Go.DataLayer.BaseClasses.DataLayersBases;
using Scan2Go.Entity.Definitions;

namespace Scan2Go.DataLayer.DefDetailSchemaDataLayer;

internal class DefDetailSchemaDMLOperations : Scan2GoDataLayerBase
{
    public DefDetailSchemaDMLOperations(DataLayerBase dataLayerBase) : base(dataLayerBase)
    {
    }

    public void DeleteDefDetailSchema(DefDetailSchema defDetailSchema)
    {
        new GeneralDMLOperations(this).DeleteEntity(defDetailSchema);
    }

    internal void SaveDefDetailSchema(DefDetailSchema defDetailSchema)
    {
        new GeneralDMLOperations(this).SaveEntity(defDetailSchema);
    }
}