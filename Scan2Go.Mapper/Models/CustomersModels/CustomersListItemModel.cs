using Utility.Bases;

namespace Scan2Go.Mapper.Models.CustomersModels;

public class CustomersListItemModel : ISelectableItem
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
	public int RowId { get; set; }

	#region ISelectableItem Members

	public int Id => CustomerId;
	public string Label => string.Empty;
	public string Value => string.Empty;

	 #endregion ISelectableItem Members
}
