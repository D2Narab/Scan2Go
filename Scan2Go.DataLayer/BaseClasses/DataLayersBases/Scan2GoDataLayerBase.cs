using Scan2Go.Enums;
using DataLayer.Base.DatabaseFactory;

namespace Scan2Go.DataLayer.BaseClasses.DataLayersBases;

public class Scan2GoDataLayerBase : DataLayerBase
{
    public Scan2GoDataLayerBase() : base(DbNames.Default)
    {

    }



    public Scan2GoDataLayerBase(DataLayerBase dataLayerBase) : base(dataLayerBase)
    {
    }
}