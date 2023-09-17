namespace Scan2Go.Entity.Users
{
    public class UsersSearchCriteria
    {
        public string OrderByColumn { get; set; }
        public int Range { get; set; }
        public string SortType { get; set; }
        public int StartFrom { get; set; }
    }
}