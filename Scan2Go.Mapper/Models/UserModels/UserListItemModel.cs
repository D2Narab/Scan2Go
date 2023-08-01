using Scan2Go.Mapper.BaseClasses;
using Utility.Bases;

namespace Scan2Go.Mapper.Models.UserModels;

public class UserListItemModel : BaseModel, ISelectableItem
{
    public bool IsActive { get; set; }
    public string UserCode { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string UserSurname { get; set; }

    #region ISelectableItem Members

    public int Id => UserId;
    public string Label => UserName;
    public string Value => UserName;

    #endregion ISelectableItem Members

    #region BaseModel Members

    public override int PkId => UserId;

    #endregion BaseModel Members
}