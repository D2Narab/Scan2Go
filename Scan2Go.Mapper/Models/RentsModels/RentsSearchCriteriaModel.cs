namespace Scan2Go.Mapper.Models.RentsModels;

public class RentsSearchCriteriaModel
{
	public string OrderByColumn { get; set; }
	public int Range { get; set; }
	public string SortType { get; set; }
	public int StartFrom { get; set; }
}
