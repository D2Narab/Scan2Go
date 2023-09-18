using Utility.Bases;

namespace Scan2Go.Mapper.Models.RentsModels;

public class RentsListItemModel : ISelectableItem
{
	public string Car { get; set; }
	public string Customer { get; set; }
	public bool HasInsurance { get; set; }
	public string RentEndDate { get; set; } 
	public int RentId { get; set; }
	public string RentStartDate { get; set; } 
	public string TotalCharge { get; set; } 
	public int RowId { get; set; }

	#region ISelectableItem Members

	public int Id => RentId;
	public string Label => string.Empty;
	public string Value => string.Empty;

	 #endregion ISelectableItem Members
}
