using Scan2Go.DataLayer.BaseClasses.SelectOperationBases;
using Scan2Go.DataLayer.CarsDataLayer;
using Scan2Go.Entity.Cars;
using Scan2Go.Enums;
using Scan2Go.Enums.Properties;
using Scan2Go.Facade;
using System.Collections.Generic;
using System.Data;
using Utility.Bases.EntityBases;
using Utility.Bases.EntityBases.Facade;
using Utility.Core;
using Utility.Enum;
using Utility.Extensions;

namespace Facade.CarsFacade;

public class CarsFacade : FacadeBase
{
	public CarsFacade (LanguageEnum languageEnum) : base(languageEnum)
	{
	}

	public OperationResult DeleteCars(Cars cars)
	{
		return new CarsDAO().DeleteCars(cars);
	}

	public Cars GetCars(int carsId)
	{
		DataRow drCars = new Scan2GoSelectOperations().GetEntityDataRow<Cars>(carsId);
		Cars cars = FillCars(drCars);

		return cars;
	}

	public ListSourceBase GetCarsForList(CarsSearchCriteria carsSearchCriteria)
	{
		CriteriaResult criteriaResult = new CarsDAO().GetCarsForList(carsSearchCriteria);

		ListSourceBase listSourceBase = new ListSourceBase();

		listSourceBase.ListItemBases = new List<ListItemBase>();
		listSourceBase.TotalRecordCount = criteriaResult.TotalRecordCount;
		listSourceBase.ListItemType = typeof(CarsListItem);

		listSourceBase.RecordInfo = EnumMethods.GetResourceString(nameof(MessageStrings.CarsList), this.languageEnum);

		foreach (DataRow dr in criteriaResult.CriteriaDataTable.Rows)
		{
			listSourceBase.ListItemBases.Add(FillCarsListItem(dr));
		}

		listSourceBase.ListCaptionBases = new ListCaptionBasesFacade(this.languageEnum).GetCaptions(listSourceBase);

		return listSourceBase;
	}

	public OperationResult SaveCars (Cars cars)
	{
		return new CarsDAO().SaveCars(cars);
	}

	private Cars FillCars(DataRow row)
	{
		if (row == null) { return null; }

		var cars = new Cars();
		cars.DefCarBrands = new DefinitionFacade(this.languageEnum).GetDefinition(row.AsInt(Cars.Field.CarBrandId),TableName.Def_CarBrands.ToString());
		cars.CarId = row.AsInt(Cars.Field.CarId);
		cars.DefCarModels = new DefinitionFacade(this.languageEnum).GetDefinition(row.AsInt(Cars.Field.CarModelId),TableName.Def_CarModels.ToString());
		cars.CarName = row.AsString(Cars.Field.CarName);
		cars.CarOwner = row.AsString(Cars.Field.CarOwner);
		cars.CarYear = row.AsInt(Cars.Field.CarYear);
		cars.CurrentPlace = row.AsString(Cars.Field.CurrentPlace);
		cars.IsRented = row.AsBool(Cars.Field.IsRented);
		cars.PurchaseDate = row.AsDateTime(Cars.Field.PurchaseDate);

		cars.ChangeState(false);

		return cars;
	}

	private CarsListItem FillCarsListItem(DataRow dr)
	{
		if (dr == null) { return null; }

		var carsListItem = new CarsListItem();
		
		carsListItem.CarBrand =new DefinitionFacade(this.languageEnum).GetDefinition(dr.AsInt(Cars.Field.CarBrandId), TableName.Def_CarBrands.ToString()).NameValue;
		carsListItem.CarId = dr.AsInt(Cars.Field.CarId);

		carsListItem.CarModel =new DefinitionFacade(this.languageEnum).GetDefinition(dr.AsInt(Cars.Field.CarModelId), TableName.Def_CarModels.ToString()).NameValue;
		carsListItem.CarName = dr.AsString(Cars.Field.CarName);

		carsListItem.CarOwner = dr.AsString(Cars.Field.CarOwner);

		carsListItem.CarYear = dr.AsInt(Cars.Field.CarYear);

		carsListItem.CurrentPlace = dr.AsString(Cars.Field.CurrentPlace);

		carsListItem.IsRented = dr.AsBool(Cars.Field.IsRented);

		carsListItem.PurchaseDate = dr.AsDateTime(Cars.Field.PurchaseDate).ToShortDateString();


		return carsListItem;
	}
}
