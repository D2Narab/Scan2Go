namespace Scan2Go.Mapper.Models.CarsModels;

public class CarsListItemModel
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
	public int RowId { get; set; }
}
