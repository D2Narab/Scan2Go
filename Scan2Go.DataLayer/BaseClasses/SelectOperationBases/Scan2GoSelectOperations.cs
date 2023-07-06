using Scan2Go.Enums;
using DataLayer.Base.DatabaseFactory;
using DataLayer.Base.GeneralDataLayer;

namespace Scan2Go.DataLayer.BaseClasses.SelectOperationBases;

public class Scan2GoSelectOperations : GeneralSelectOperations
{
    public Scan2GoSelectOperations(DataLayerBase dataLayerBase) : base(dataLayerBase)
    {
    }

    public Scan2GoSelectOperations() : base(DbNames.Default)
    {

    }
}