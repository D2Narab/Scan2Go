namespace Scan2Go.Mapper.Models.UserModels
{
    public class UsersSearchCriteriaModel
    {
        public string OrderByColumn { get; set; }
        public int Range { get; set; }
        public string SortType { get; set; }
        public int StartFrom { get; set; }
    }
}