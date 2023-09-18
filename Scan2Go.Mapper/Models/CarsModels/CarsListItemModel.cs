using Utility.Bases;

namespace Scan2Go.Mapper.Models.CarsModels;

public class CarsListItemModel : ISelectableItem
{
    public string CarBrand { get; set; }
    public int CarId { get; set; }
    public string CarModel { get; set; }
    public string CarName { get; set; }
    public string CarOwner { get; set; }
    public string CarYear { get; set; }
    public string CurrentPlace { get; set; }
    public bool IsRented { get; set; }
    public string PurchaseDate { get; set; }
    public int RowId { get; set; }

    #region ISelectableItem Members

    public int Id => CarId;
    public string Label => CarName;
    public string Value => CarName;

    #endregion
}
