using Utility.Bases.EntityBases;
using Utility.Bases.EntityBases.DefinitionBases;

namespace Scan2Go.Entity.Rents;

public class RentsListItem : ListItemBase
{
	public string Car { get; set; }
	public string Customer { get; set; }
	public bool HasInsurance { get; set; }
	public string RentEndDate { get; set; }
	public int RentId { get; set; }
	public string RentStartDate { get; set; }
	public string TotalCharge { get; set; }

	#region ListItemBase

	public override int RowId => RentId;

	#endregion ListItemBase

	public class Field : DefinitionFieldEntityBase
	{
	}
}
