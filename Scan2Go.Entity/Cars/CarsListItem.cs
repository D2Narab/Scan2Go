using Utility.Bases.EntityBases;
using Utility.Bases.EntityBases.DefinitionBases;

namespace Scan2Go.Entity.Cars;

public class CarsListItem : ListItemBase
{
	public string CarBrand { get; set; }
	public int CarId { get; set; }
	public string CarModel { get; set; }
	public string CarName { get; set; }
	public string CarOwner { get; set; }
	public int CarYear { get; set; }
	public string CurrentPlace { get; set; }
	public bool IsRented { get; set; }
	public string PurchaseDate { get; set; }

	#region ListItemBase

	public override int RowId => CarId;

	#endregion ListItemBase

	public class Field : DefinitionFieldEntityBase
	{
		public const string CarBrand = "CarBrand";
		public const string CarModel = "CarModel";
	}
}
