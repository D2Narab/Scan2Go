using Scan2Go.DataLayer.BaseClasses.DataLayersBases;
using Scan2Go.Entity.Rents;
using DataLayer.Base.DatabaseFactory;
using DataLayer.Base.GeneralDataLayer;

namespace Scan2Go.DataLayer.RentsDataLayer;
internal class RentsDMLOperations : Scan2GoDataLayerBase
{
	public  RentsDMLOperations(DataLayerBase dataLayerBase) : base(dataLayerBase)
	{
	}

	internal void DeleteRents(Rents rents)
	{
		new GeneralDMLOperations(this).DeleteEntity(rents);
	}

	internal void SaveRents(Rents rents)
	{
		new GeneralDMLOperations(this).SaveEntity(rents);
	}
}
