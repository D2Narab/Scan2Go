using Scan2Go.Entity.Definitions;
using Scan2Go.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Utility.Extensions;
using Utility.Bases.EntityBases;
using Utility.Bases.EntityBases.DefinitionBases;
using Utility.Core.DataLayer;

namespace Scan2Go.Entity.Cars; 
[Serializable]
[DebuggerDisplay("{GetPrimaryKeyName}: {PrimaryKeyValue}, /*Enter whatever you want to see on the debugger here*/")]
public class Cars: EntityStateBase
{
	#region FieldNames

	public class Field : DefinitionFieldEntityBase
	{
		public const string CarBrandId="CarBrandId";
		public const string CarId="CarId";
		public const string CarModelId="CarModelId";
		public const string CarName="CarName";
		public const string CarOwner="CarOwner";
		public const string CarYear="CarYear";
		public const string CurrentPlace="CurrentPlace";
		public const string IsRented="IsRented";
		public const string PurchaseDate="PurchaseDate";
	}

	#endregion FieldNames

	#region Privates of Cars

	private Definition  _defCarBrands;
	private int _carId;
	private Definition  _defCarModels;
	private string _carName;
	private string _carOwner;
	private int _carYear;
	private string _currentPlace;
	private bool _isRented;
	private DateTime _purchaseDate;

	private CarsState _stateCars;

	protected override IState _state => _stateCars;

	#endregion Privates of Cars

	#region Constructor of Cars

	public Cars()
	{
		_stateCars= new CarsState();
	}

	#endregion Constructor of Cars

	#region Properties of Cars

	public Definition DefCarBrands {get => _defCarBrands; set => SetProperty(ref _defCarBrands, value, _stateCars, nameof(_stateCars.CarBrandId));}
	public int CarId { get => _carId; set => _carId = value; }
	public Definition DefCarModels {get => _defCarModels; set => SetProperty(ref _defCarModels, value, _stateCars, nameof(_stateCars.CarModelId));}
	public string CarName { get => _carName; set => SetProperty(ref _carName , value, _stateCars , nameof(_stateCars.CarName));}
	public string CarOwner { get => _carOwner; set => SetProperty(ref _carOwner , value, _stateCars , nameof(_stateCars.CarOwner));}
	public int CarYear { get => _carYear; set => SetProperty(ref _carYear , value, _stateCars ,nameof(_stateCars.CarYear));}
	public string CurrentPlace { get => _currentPlace; set => SetProperty(ref _currentPlace , value, _stateCars , nameof(_stateCars.CurrentPlace));}
	public bool IsRented { get => _isRented; set => SetProperty(ref _isRented, value, _stateCars, nameof(_stateCars.IsRented));}
	public DateTime PurchaseDate { get => _purchaseDate; set => SetProperty(ref _purchaseDate, value, _stateCars,nameof(_stateCars.PurchaseDate));}

	#endregion Properties of Cars


	#region AUTO methods

	public override int PrimaryKeyValue { get => CarId; set => CarId=value; }

	public override List<DatabaseParameter> GetEntityDbParameters(bool isStateCheck = true)
	{
		this.ClearSqlParameters();

		if (isStateCheck && _stateCars.CarBrandId)
		{
			AddParam(Field.CarBrandId, DbType.Int32, DefCarBrands.GetSQLParamPrimaryKeyValue()); 
		}

		if (isStateCheck && _stateCars.CarId)
		{
			AddParam(Field.CarId,DbType.Int32, CarId);
		}

		if (isStateCheck && _stateCars.CarModelId)
		{
			AddParam(Field.CarModelId, DbType.Int32, DefCarModels.GetSQLParamPrimaryKeyValue()); 
		}

		if (isStateCheck && _stateCars.CarName)
		{
			AddParam(Field.CarName,DbType.String, CarName);
		}

		if (isStateCheck && _stateCars.CarOwner)
		{
			AddParam(Field.CarOwner,DbType.String, CarOwner);
		}

		if (isStateCheck && _stateCars.CarYear)
		{
			AddParam(Field.CarYear,DbType.Int32, CarYear);
		}

		if (isStateCheck && _stateCars.CurrentPlace)
		{
			AddParam(Field.CurrentPlace,DbType.String, CurrentPlace);
		}

		if (isStateCheck && _stateCars.IsRented)
		{
			AddParam(Field.IsRented,DbType.Boolean, IsRented);
		}

		if (isStateCheck && _stateCars.PurchaseDate)
		{
			AddParam(Field.PurchaseDate,DbType.DateTime, PurchaseDate);
		}

		return this.GetSqlParameters();
	}

	public override DataLayerEnumBase GetPrimaryKeyName() => PrimaryKey.CarId;

	public override DataLayerEnumBase GetTableName() => TableName.Cars;

	#endregion AUTO methods

	#region OtherProperties

	#endregion OtherProperties
}

#region State
public class CarsState: IState
{
	 public bool CarBrandId {get; set;}
	 public bool CarId {get; set;}
	 public bool CarModelId {get; set;}
	 public bool CarName {get; set;}
	 public bool CarOwner {get; set;}
	 public bool CarYear {get; set;}
	 public bool CurrentPlace {get; set;}
	 public bool IsRented {get; set;}
	 public bool PurchaseDate {get; set;}

	public void ChangeState(bool state)
	{ 
		CarBrandId= state ;
		CarId= state ;
		CarModelId= state ;
		CarName= state ;
		CarOwner= state ;
		CarYear= state ;
		CurrentPlace= state ;
		IsRented= state ;
		PurchaseDate= state ;
	}

	public bool IsDirty()
	{ 
		 return 
		CarBrandId ||CarId ||CarModelId ||CarName ||CarOwner ||CarYear ||CurrentPlace ||IsRented ||PurchaseDate ;
	}

	#endregion State
}
