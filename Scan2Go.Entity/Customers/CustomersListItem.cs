using Utility.Bases.EntityBases;
using Utility.Bases.EntityBases.DefinitionBases;

namespace Scan2Go.Entity.Customers;

public class CustomersListItem : ListItemBase
{
	public string BirthDate { get; set; }
	public string CityName { get; set; }
	public string Country { get; set; }
	public int CustomerId { get; set; }
	public string CustomerName { get; set; }
	public string CustomerSurname { get; set; }
	public string DrivingLicenseNumber { get; set; }
	public string EMail { get; set; }
	public string HomeAdress { get; set; }
	public string IdNumber { get; set; }
	public string MobilePhoneNumber { get; set; }
	public string Nationality { get; set; }
	public string PassportNumber { get; set; }

	#region ListItemBase

	public override int RowId => CustomerId;

	#endregion ListItemBase

	public class Field : DefinitionFieldEntityBase
	{
	}
}
