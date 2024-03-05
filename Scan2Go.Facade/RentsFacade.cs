using Scan2Go.DataLayer.BaseClasses.SelectOperationBases;
using Scan2Go.DataLayer.RentsDataLayer;
using Scan2Go.Entity.Rents;
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

namespace Facade.RentsFacade;

public class RentsFacade : FacadeBase
{
	public RentsFacade (LanguageEnum languageEnum) : base(languageEnum)
	{
	}

	public OperationResult DeleteRents(Rents rents)
	{
		return new RentsDAO().DeleteRents(rents);
	}

	public Rents GetRents(int rentsId)
	{
		DataRow drRents = new Scan2GoSelectOperations().GetEntityDataRow<Rents>(rentsId);
		Rents rents = FillRents(drRents);

		return rents;
	}

	public ListSourceBase GetRentsForList(RentsSearchCriteria rentsSearchCriteria)
	{
		CriteriaResult criteriaResult = new RentsDAO().GetRentsForList(rentsSearchCriteria);

		ListSourceBase listSourceBase = new ListSourceBase();

		listSourceBase.ListItemBases = new List<ListItemBase>();
		listSourceBase.TotalRecordCount = criteriaResult.TotalRecordCount;
		listSourceBase.ListItemType = typeof(RentsListItem);

		listSourceBase.RecordInfo = EnumMethods.GetResourceString(nameof(MessageStrings.RentsList), this.languageEnum);

		foreach (DataRow dr in criteriaResult.CriteriaDataTable.Rows)
		{
			listSourceBase.ListItemBases.Add(FillRentsListItem(dr));
		}

		listSourceBase.ListCaptionBases = new ListCaptionBasesFacade(this.languageEnum).GetCaptions(listSourceBase);

		return listSourceBase;
	}

	public OperationResult SaveRents (Rents rents)
	{
		return new RentsDAO().SaveRents(rents);
	}

	private Rents FillRents(DataRow row)
	{
		if (row == null) { return null; }

		var rents = new Rents();
		rents.CarId = row.AsInt(Rents.Field.CarId);
		rents.CustomerId = row.AsInt(Rents.Field.CustomerId);
		rents.HasInsurance = row.AsBool(Rents.Field.HasInsurance);
		rents.RentEndDate = row.AsDateTime(Rents.Field.RentEndDate);
		rents.RentId = row.AsInt(Rents.Field.RentId);
		rents.RentStartDate = row.AsDateTime(Rents.Field.RentStartDate);
		rents.TotalCharge = row.AsDouble(Rents.Field.TotalCharge);

		rents.ChangeState(false);

		return rents;
	}

	private RentsListItem FillRentsListItem(DataRow dr)
	{
		if (dr == null) { return null; }

		var rentsListItem = new RentsListItem();
		
		rentsListItem.Car = new CarsFacade.CarsFacade(this.languageEnum).GetCars(dr.AsInt(Rents.Field.CarId)).CarName;

		rentsListItem.Customer = new CustomersFacade.CustomersFacade(this.languageEnum)
            .GetCustomers(dr.AsInt(Rents.Field.CustomerId)).CustomerFullName;

		rentsListItem.HasInsurance = dr.AsBool(Rents.Field.HasInsurance);

		rentsListItem.RentEndDate = dr.AsDateTime(Rents.Field.RentEndDate).ToShortDateString();

		rentsListItem.RentId = dr.AsInt(Rents.Field.RentId);

		rentsListItem.RentStartDate = dr.AsDateTime(Rents.Field.RentStartDate).ToShortDateString();

		rentsListItem.TotalCharge = dr.AsString(Rents.Field.TotalCharge);


		return rentsListItem;
	}

    public Rents GetRentByCustomerName(string customerName)
    {
        DataRow drRents = new RentsDAO().GetRentByCustomerName(customerName);
        Rents rents = FillRents(drRents);

        return rents;
    }

    public Rents? GetRentByPassportNumber(string? documentNumber)
    {
        DataRow drRents = new RentsDAO().GetRentByPassportNumber(documentNumber);
        Rents rents = FillRents(drRents);

        return rents;
    }
}
