namespace Scan2Go.Mapper.Models.CustomersModels;

public class CustomersSearchCriteriaModel
{
	public string OrderByColumn { get; set; }
	public int Range { get; set; }
	public string SortType { get; set; }
	public int StartFrom { get; set; }
}
