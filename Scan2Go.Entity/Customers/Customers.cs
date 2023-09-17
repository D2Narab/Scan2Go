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

namespace Scan2Go.Entity.Customers; 
[Serializable]
[DebuggerDisplay("{GetPrimaryKeyName}: {PrimaryKeyValue}, /*Enter whatever you want to see on the debugger here*/")]
public class Customers: EntityStateBase
{
	#region FieldNames

	public class Field : DefinitionFieldEntityBase
	{
		public const string BirthDate="BirthDate";
		public const string CityName="CityName";
		public const string Country="Country";
		public const string CustomerId="CustomerId";
		public const string CustomerName="CustomerName";
		public const string CustomerSurname="CustomerSurname";
		public const string DrivingLicenseNumber="DrivingLicenseNumber";
		public const string EMail="EMail";
		public const string HomeAdress="HomeAdress";
		public const string IdNumber="IdNumber";
		public const string MobilePhoneNumber="MobilePhoneNumber";
		public const string Nationality="Nationality";
		public const string PassportNumber="PassportNumber";
	}

	#endregion FieldNames

	#region Privates of Customers

	private DateTime _birthDate;
	private string _cityName;
	private string _country;
	private int _customerId;
	private string _customerName;
	private string _customerSurname;
	private string _drivingLicenseNumber;
	private string _eMail;
	private string _homeAdress;
	private string _idNumber;
	private string _mobilePhoneNumber;
	private string _nationality;
	private string _passportNumber;

	private CustomersState _stateCustomers;

	protected override IState _state => _stateCustomers;

	#endregion Privates of Customers

	#region Constructor of Customers

	public Customers()
	{
		_stateCustomers= new CustomersState();
	}

	#endregion Constructor of Customers

	#region Properties of Customers

	public DateTime BirthDate { get => _birthDate; set => SetProperty(ref _birthDate, value, _stateCustomers,nameof(_stateCustomers.BirthDate));}
	public string CityName { get => _cityName; set => SetProperty(ref _cityName , value, _stateCustomers , nameof(_stateCustomers.CityName));}
	public string Country { get => _country; set => SetProperty(ref _country , value, _stateCustomers , nameof(_stateCustomers.Country));}
	public int CustomerId { get => _customerId; set => _customerId = value; }
	public string CustomerName { get => _customerName; set => SetProperty(ref _customerName , value, _stateCustomers , nameof(_stateCustomers.CustomerName));}
	public string CustomerSurname { get => _customerSurname; set => SetProperty(ref _customerSurname , value, _stateCustomers , nameof(_stateCustomers.CustomerSurname));}
	public string DrivingLicenseNumber { get => _drivingLicenseNumber; set => SetProperty(ref _drivingLicenseNumber , value, _stateCustomers , nameof(_stateCustomers.DrivingLicenseNumber));}
	public string EMail { get => _eMail; set => SetProperty(ref _eMail , value, _stateCustomers , nameof(_stateCustomers.EMail));}
	public string HomeAdress { get => _homeAdress; set => SetProperty(ref _homeAdress , value, _stateCustomers , nameof(_stateCustomers.HomeAdress));}
	public string IdNumber { get => _idNumber; set => SetProperty(ref _idNumber , value, _stateCustomers , nameof(_stateCustomers.IdNumber));}
	public string MobilePhoneNumber { get => _mobilePhoneNumber; set => SetProperty(ref _mobilePhoneNumber , value, _stateCustomers , nameof(_stateCustomers.MobilePhoneNumber));}
	public string Nationality { get => _nationality; set => SetProperty(ref _nationality , value, _stateCustomers , nameof(_stateCustomers.Nationality));}
	public string PassportNumber { get => _passportNumber; set => SetProperty(ref _passportNumber , value, _stateCustomers , nameof(_stateCustomers.PassportNumber));}

	#endregion Properties of Customers


	#region AUTO methods

	public override int PrimaryKeyValue { get => CustomerId; set => CustomerId=value; }

	public override List<DatabaseParameter> GetEntityDbParameters(bool isStateCheck = true)
	{
		this.ClearSqlParameters();

		if (isStateCheck && _stateCustomers.BirthDate)
		{
			AddParam(Field.BirthDate,DbType.Date, BirthDate);
		}

		if (isStateCheck && _stateCustomers.CityName)
		{
			AddParam(Field.CityName,DbType.String, CityName);
		}

		if (isStateCheck && _stateCustomers.Country)
		{
			AddParam(Field.Country,DbType.String, Country);
		}

		if (isStateCheck && _stateCustomers.CustomerId)
		{
			AddParam(Field.CustomerId,DbType.Int32, CustomerId);
		}

		if (isStateCheck && _stateCustomers.CustomerName)
		{
			AddParam(Field.CustomerName,DbType.String, CustomerName);
		}

		if (isStateCheck && _stateCustomers.CustomerSurname)
		{
			AddParam(Field.CustomerSurname,DbType.String, CustomerSurname);
		}

		if (isStateCheck && _stateCustomers.DrivingLicenseNumber)
		{
			AddParam(Field.DrivingLicenseNumber,DbType.String, DrivingLicenseNumber);
		}

		if (isStateCheck && _stateCustomers.EMail)
		{
			AddParam(Field.EMail,DbType.String, EMail);
		}

		if (isStateCheck && _stateCustomers.HomeAdress)
		{
			AddParam(Field.HomeAdress,DbType.String, HomeAdress);
		}

		if (isStateCheck && _stateCustomers.IdNumber)
		{
			AddParam(Field.IdNumber,DbType.String, IdNumber);
		}

		if (isStateCheck && _stateCustomers.MobilePhoneNumber)
		{
			AddParam(Field.MobilePhoneNumber,DbType.String, MobilePhoneNumber);
		}

		if (isStateCheck && _stateCustomers.Nationality)
		{
			AddParam(Field.Nationality,DbType.String, Nationality);
		}

		if (isStateCheck && _stateCustomers.PassportNumber)
		{
			AddParam(Field.PassportNumber,DbType.String, PassportNumber);
		}

		return this.GetSqlParameters();
	}

	public override DataLayerEnumBase GetPrimaryKeyName() => PrimaryKey.CustomerId;

	public override DataLayerEnumBase GetTableName() => TableName.Customers;

	#endregion AUTO methods

	#region OtherProperties

	#endregion OtherProperties
}

#region State
public class CustomersState: IState
{
	 public bool BirthDate {get; set;}
	 public bool CityName {get; set;}
	 public bool Country {get; set;}
	 public bool CustomerId {get; set;}
	 public bool CustomerName {get; set;}
	 public bool CustomerSurname {get; set;}
	 public bool DrivingLicenseNumber {get; set;}
	 public bool EMail {get; set;}
	 public bool HomeAdress {get; set;}
	 public bool IdNumber {get; set;}
	 public bool MobilePhoneNumber {get; set;}
	 public bool Nationality {get; set;}
	 public bool PassportNumber {get; set;}

	public void ChangeState(bool state)
	{ 
		BirthDate= state ;
		CityName= state ;
		Country= state ;
		CustomerId= state ;
		CustomerName= state ;
		CustomerSurname= state ;
		DrivingLicenseNumber= state ;
		EMail= state ;
		HomeAdress= state ;
		IdNumber= state ;
		MobilePhoneNumber= state ;
		Nationality= state ;
		PassportNumber= state ;
	}

	public bool IsDirty()
	{ 
		 return 
		BirthDate ||CityName ||Country ||CustomerId ||CustomerName ||CustomerSurname ||DrivingLicenseNumber ||EMail ||HomeAdress ||IdNumber ||MobilePhoneNumber ||Nationality ||PassportNumber ;
	}

	#endregion State
}
