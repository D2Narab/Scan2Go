using Scan2Go.Mapper.Models.DefinitionModels;
using System;

namespace Scan2Go.Mapper.Models.CarsModels;

public class CarsModel
{
	public DefinitionModel DefCarBrands { get; set; }
	public int CarId { get; set; }
	public DefinitionModel DefCarModels { get; set; }
	public string CarName { get; set; }
	public string CarOwner { get; set; }
	public int CarYear { get; set; }
	public string CurrentPlace { get; set; }
	public bool IsRented { get; set; }
	public DateTime PurchaseDate { get; set; }
}
