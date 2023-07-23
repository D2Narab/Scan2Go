using Scan2Go.DataLayer.BaseClasses.SelectOperationBases;
using Scan2Go.Entity.Users;
using Scan2Go.Enums;
using System.Data;
using Scan2Go.DataLayer.UsersDataLayer;
using Utility.Bases.CollectionBases;
using Utility.Bases.EntityBases.Facade;
using Utility.Extensions;
using Scan2Go.Enums.Properties;
using Utility.Bases.EntityBases;
using Utility.Core;
using Utility.Enum;

namespace Scan2Go.Facade;

public class UsersFacade : FacadeBase
{
    public UsersFacade(Utility.Enum.LanguageEnum languageEnum) : base(languageEnum)
    {
    }

    public Users GetUser(int userId)
    {
        return GetUsersList().FirstOrDefault(p => p.UserId == userId);
    }

    public Users GetUsers(int userId)
    {
        DataRow drUsers = new Scan2GoSelectOperations().GetEntityDataRow<Users>(userId);
        Users users = FillUsers(drUsers);

        return users;
    }

    public Users Login(string userName, string password)
    {
        DataRow dataRow = new UsersDAO().Login(userName, password);

        Users users = FillUsers(dataRow);

        return users;
    }

    private Users FillUsers(DataRow row)
    {
        var users = new Users();

        users.Password = row.AsString(Users.Field.Password);
        users.UserCode = row.AsString(Users.Field.UserCode);
        users.UserId = row.AsInt(Users.Field.UserId);
        users.UserName = row.AsString(Users.Field.UserName);
        users.UserSurname = row.AsString(Users.Field.UserSurname);
        users.IsActive = row.AsBool(Users.Field.IsActive);

        return users;
    }

    public EntityCollectionBase<Users> GetUsersList()
    {
        EntityCollectionBase<Users> entityCollectionBase = _cacheManager.GetData(this.languageEnum.ToString() + "_" + CacheKey.Users.ToString()) as EntityCollectionBase<Users>;

        if (entityCollectionBase == null)
        {
            entityCollectionBase = new EntityCollectionBase<Users>();

            DataTable dataTable = new Scan2GoSelectOperations().GetEntityDataTable<Users>();

            foreach (DataRow row in dataTable.Rows)
            {
                var userBrief = FillUsers(row);

                entityCollectionBase.Add(userBrief);
            }

            _cacheManager.SetData(this.languageEnum.ToString() + "_" + CacheKey.Users.ToString(), entityCollectionBase);
        }

        return entityCollectionBase;
    }

    public ListSourceBase GetUsersListItems()
    {
        DataTable dataTable = new UsersDAO().GetUsersListItems();

        ListSourceBase listSourceBase = new ListSourceBase();
        listSourceBase.ListItemBases = new List<ListItemBase>();
        listSourceBase.ListItemType = typeof(UserListItem);
        listSourceBase.RecordInfo = EnumMethods.GetResourceString(nameof(MessageStrings.UserListRecordInfo), this.languageEnum);

        foreach (DataRow dataRow in dataTable.Rows)
        {
            listSourceBase.ListItemBases.Add(FillUserListItem(dataRow));
        }

        listSourceBase.TotalRecordCount = dataTable.Rows.Count;

        return listSourceBase;
    }

    private UserListItem FillUserListItem(DataRow row)
    {
        if (row == null) { return null; }

        UserListItem users = new UserListItem();

        users.UserCode = row.AsString(Users.Field.UserCode);
        users.UserId = row.AsInt(Users.Field.UserId);
        users.UserName = row.AsString(Users.Field.UserName);
        users.UserSurname = row.AsString(Users.Field.UserSurname);
        users.IsActive = row.AsBool(Users.Field.IsActive);

        return users;
    }

    public OperationResult SaveUsers(Users users)
    {
        OperationResult operationResult = new UsersDAO().SaveUsers(users);

        return operationResult;
    }
}