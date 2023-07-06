using Scan2Go.DataLayer.BaseClasses.DataLayersBases;
using DataLayer.Base.DatabaseFactory;
using DataLayer.Base.GeneralDataLayer;
using Utility.Bases;

namespace Scan2Go.DataLayer.ListCaptionBaseDataLater;

internal class ListCaptionBaseDMLOperations : Scan2GoDataLayerBase
{
    public ListCaptionBaseDMLOperations(DataLayerBase dataLayerBase) : base(dataLayerBase)
    {
    }

    internal void SaveListCaptionBase(ListCaptionBase listCaptionBase)
    {
        new GeneralDMLOperations(this).SaveEntity(listCaptionBase);
    }
}