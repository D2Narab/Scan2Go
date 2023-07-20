using Utility.Bases.EntityBases;

namespace Scan2Go.Entity.Users;

[Serializable]
public class UserListItem : ListItemBase
{
    public override int RowId => UserId;
    public string UserCode { get; set; }

    public int UserId { get; set; }

    public string UserName { get; set; }

    public string UserSurname { get; set; }
}