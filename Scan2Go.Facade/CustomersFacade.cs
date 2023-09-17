using Scan2Go.DataLayer.BaseClasses.SelectOperationBases;
using Scan2Go.DataLayer.CustomersDataLayer;
using Scan2Go.Entity.Customers;
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

namespace Facade.CustomersFacade;

public class CustomersFacade : FacadeBase
{
	public CustomersFacade (LanguageEnum languageEnum) : base(languageEnum)
	{
	}

	public OperationResult DeleteCustomers(Customers customers)
	{
		return new CustomersDAO().DeleteCustomers(customers);
	}

	public Customers GetCustomers(int customersId)
	{
		DataRow drCustomers = new Scan2GoSelectOperations().GetEntityDataRow<Customers>(customersId);
		Customers customers = FillCustomers(drCustomers);

		return customers;
	}

	public ListSourceBase GetCustomersForList(CustomersSearchCriteria customersSearchCriteria)
	{
		CriteriaResult criteriaResult = new CustomersDAO().GetCustomersForList(customersSearchCriteria);

		ListSourceBase listSourceBase = new ListSourceBase();

		listSourceBase.ListItemBases = new List<ListItemBase>();
		listSourceBase.TotalRecordCount = criteriaResult.TotalRecordCount;
		listSourceBase.ListItemType = typeof(CustomersListItem);

		listSourceBase.RecordInfo = EnumMethods.GetResourceString(nameof(MessageStrings.CustomersList), this.languageEnum);

		foreach (DataRow dr in criteriaResult.CriteriaDataTable.Rows)
		{
			listSourceBase.ListItemBases.Add(FillCustomersListItem(dr));
		}

		listSourceBase.ListCaptionBases = new ListCaptionBasesFacade(this.languageEnum).GetCaptions(listSourceBase);

		return listSourceBase;
	}

	public OperationResult SaveCustomers (Customers customers)
	{
		return new CustomersDAO().SaveCustomers(customers);
	}

	private Customers FillCustomers(DataRow row)
	{
		if (row == null) { return null; }

		var customers = new Customers();
		customers.BirthDate = row.AsDateTime(Customers.Field.BirthDate);
		customers.CityName = row.AsString(Customers.Field.CityName);
		customers.Country = row.AsString(Customers.Field.Country);
		customers.CustomerId = row.AsInt(Customers.Field.CustomerId);
		customers.CustomerName = row.AsString(Customers.Field.CustomerName);
		customers.CustomerSurname = row.AsString(Customers.Field.CustomerSurname);
		customers.DrivingLicenseNumber = row.AsString(Customers.Field.DrivingLicenseNumber);
		customers.EMail = row.AsString(Customers.Field.EMail);
		customers.HomeAdress = row.AsString(Customers.Field.HomeAdress);
		customers.IdNumber = row.AsString(Customers.Field.IdNumber);
		customers.MobilePhoneNumber = row.AsString(Customers.Field.MobilePhoneNumber);
		customers.Nationality = row.AsString(Customers.Field.Nationality);
		customers.PassportNumber = row.AsString(Customers.Field.PassportNumber);

		customers.ChangeState(false);

		return customers;
	}

	private CustomersListItem FillCustomersListItem(DataRow dr)
	{
		if (dr == null) { return null; }

		var customersListItem = new CustomersListItem();
		
		customersListItem.BirthDate = dr.AsDateTime(Customers.Field.BirthDate).ToShortDateString();

		customersListItem.CityName = dr.AsString(Customers.Field.CityName);

		customersListItem.Country = dr.AsString(Customers.Field.Country);

		customersListItem.CustomerId = dr.AsInt(Customers.Field.CustomerId);

		customersListItem.CustomerName = dr.AsString(Customers.Field.CustomerName);

		customersListItem.CustomerSurname = dr.AsString(Customers.Field.CustomerSurname);

		customersListItem.DrivingLicenseNumber = dr.AsString(Customers.Field.DrivingLicenseNumber);

		customersListItem.EMail = dr.AsString(Customers.Field.EMail);

		customersListItem.HomeAdress = dr.AsString(Customers.Field.HomeAdress);

		customersListItem.IdNumber = dr.AsString(Customers.Field.IdNumber);

		customersListItem.MobilePhoneNumber = dr.AsString(Customers.Field.MobilePhoneNumber);

		customersListItem.Nationality = dr.AsString(Customers.Field.Nationality);

		customersListItem.PassportNumber = dr.AsString(Customers.Field.PassportNumber);


		return customersListItem;
	}
}
