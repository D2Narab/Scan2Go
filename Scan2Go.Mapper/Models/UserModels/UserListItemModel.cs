using Utility.Bases;

namespace Scan2Go.Mapper.Models.UserModels;

public class UserListItemModel : ISelectableItem
{
    public string UserCode { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string UserSurname { get; set; }

    #region ISelectableItem Members

    public int Id => UserId;
    public string Label => UserName;
    public string Value => UserName;

    #endregion ISelectableItem Members
}