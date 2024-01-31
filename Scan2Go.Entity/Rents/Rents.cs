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

namespace Scan2Go.Entity.Rents; 
[Serializable]
[DebuggerDisplay("{GetPrimaryKeyName}: {PrimaryKeyValue}, /*Enter whatever you want to see on the debugger here*/")]
public class Rents: EntityStateBase
{
	#region FieldNames

	public class Field : DefinitionFieldEntityBase
	{
		public const string CarId="CarId";
		public const string CustomerId="CustomerId";
		public const string HasInsurance="HasInsurance";
		public const string RentEndDate="RentEndDate";
		public const string RentId="RentId";
		public const string RentStartDate="RentStartDate";
		public const string TotalCharge="TotalCharge";
	}

	#endregion FieldNames

	#region Privates of Rents

	private int _carId;
	private int _customerId;
	private bool _hasInsurance;
	private DateTime _rentEndDate;
	private int _rentId;
	private DateTime _rentStartDate;
	private double _totalCharge;

	private RentsState _stateRents;

	protected override IState _state => _stateRents;

	#endregion Privates of Rents

	#region Constructor of Rents

	public Rents()
	{
		_stateRents= new RentsState();
	}

	#endregion Constructor of Rents

	#region Properties of Rents

	public int CarId { get => _carId; set => SetProperty(ref _carId , value, _stateRents ,nameof(_stateRents.CarId));}
	public int CustomerId { get => _customerId; set => SetProperty(ref _customerId , value, _stateRents ,nameof(_stateRents.CustomerId));}
	public bool HasInsurance { get => _hasInsurance; set => SetProperty(ref _hasInsurance, value, _stateRents, nameof(_stateRents.HasInsurance));}
	public DateTime RentEndDate { get => _rentEndDate; set => SetProperty(ref _rentEndDate, value, _stateRents,nameof(_stateRents.RentEndDate));}
	public int RentId { get => _rentId; set => _rentId = value; }
	public DateTime RentStartDate { get => _rentStartDate; set => SetProperty(ref _rentStartDate, value, _stateRents,nameof(_stateRents.RentStartDate));}
	public double TotalCharge { get => _totalCharge; set { _totalCharge = value; } }

	#endregion Properties of Rents
	
	#region AUTO methods

	public override int PrimaryKeyValue { get => RentId; set => RentId=value; }

	public override List<DatabaseParameter> GetEntityDbParameters(bool isStateCheck = true)
	{
		this.ClearSqlParameters();

		if (isStateCheck && _stateRents.CarId)
		{
			AddParam(Field.CarId,DbType.Int32, CarId);
		}

		if (isStateCheck && _stateRents.CustomerId)
		{
			AddParam(Field.CustomerId,DbType.Int32, CustomerId);
		}

		if (isStateCheck && _stateRents.HasInsurance)
		{
			AddParam(Field.HasInsurance,DbType.Boolean, HasInsurance);
		}

		if (isStateCheck && _stateRents.RentEndDate)
		{
			AddParam(Field.RentEndDate,DbType.DateTime, RentEndDate);
		}

		if (isStateCheck && _stateRents.RentId)
		{
			AddParam(Field.RentId,DbType.Int32, RentId);
		}

		if (isStateCheck && _stateRents.RentStartDate)
		{
			AddParam(Field.RentStartDate,DbType.DateTime, RentStartDate);
		}

		if (isStateCheck && _stateRents.TotalCharge)
		{
			AddParam(Field.TotalCharge,DbType.Double, TotalCharge);
		}

		return this.GetSqlParameters();
	}

	public override DataLayerEnumBase GetPrimaryKeyName() => PrimaryKey.RentId;

	public override DataLayerEnumBase GetTableName() => TableName.Rents;

	#endregion AUTO methods

	#region OtherProperties

    public List<RentNoteItem> RentNotes = new();

    #endregion OtherProperties
}

#region State
public class RentsState: IState
{
	 public bool CarId {get; set;}
	 public bool CustomerId {get; set;}
	 public bool HasInsurance {get; set;}
	 public bool RentEndDate {get; set;}
	 public bool RentId {get; set;}
	 public bool RentStartDate {get; set;}
	 public bool TotalCharge {get; set;}

	public void ChangeState(bool state)
	{ 
		CarId= state ;
		CustomerId= state ;
		HasInsurance= state ;
		RentEndDate= state ;
		RentId= state ;
		RentStartDate= state ;
		TotalCharge= state ;
	}

	public bool IsDirty()
	{ 
		 return 
		CarId ||CustomerId ||HasInsurance ||RentEndDate ||RentId ||RentStartDate ||TotalCharge ;
	}

	#endregion State
}
