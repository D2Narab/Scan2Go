using Scan2Go.DataLayer.BaseClasses.DataLayersBases;
using Scan2Go.Entity.Cars;
using DataLayer.Base.DatabaseFactory;
using DataLayer.Base.GeneralDataLayer;

namespace Scan2Go.DataLayer.CarsDataLayer;
internal class CarsDMLOperations : Scan2GoDataLayerBase
{
	public  CarsDMLOperations(DataLayerBase dataLayerBase) : base(dataLayerBase)
	{
	}

	internal void DeleteCars(Cars cars)
	{
		new GeneralDMLOperations(this).DeleteEntity(cars);
	}

	internal void SaveCars(Cars cars)
	{
		new GeneralDMLOperations(this).SaveEntity(cars);
	}
}
