using Scan2Go.Enums;
using System.Data;
using Utility.Bases;
using Utility.Bases.EntityBases;
using Utility.Bases.EntityBases.DefinitionBases;
using Utility.Core.DataLayer;
using Utility.Enum;

namespace Scan2Go.Entity.Users;

[Serializable]
public class Users : EntityStateBase, IUser
{
    #region FieldNames

    [Serializable]
    public class Field : DefinitionFieldEntityBase
    {
        //public const string IsActive = "IsActive";
        public const string Password = "Password";
        public const string UserCode = "UserCode";
        public const string UserId = "UserId";
        public const string UserName = "UserName";
        public const string UserSurname = "UserSurname";
    }

    #endregion FieldNames

    #region Privates of Users

    private bool _isActive;
    private string _password;
    private UsersState _stateUsers;
    private string _userCode;
    private int _userId;
    private string _userName;
    private string _userSurname;
    protected override IState _state => _stateUsers;

    #endregion Privates of Users

    #region Constructor of Users

    public Users()
    {
        _stateUsers = new UsersState();
    }

    #endregion Constructor of Users

    #region Properties of Users

    public bool IsActive { get { return _isActive; } set { _isActive = value; _stateUsers.IsActive = true; } }

    public string Password
    {
        get => _password;
        set { if (_password != value) { _password = value; _stateUsers.Password = true; } }
    }

    public string UserCode
    {
        get => _userCode;
        set { if (_userCode != value) { _userCode = value; _stateUsers.UserCode = true; } }
    }

    public int UserId
    {
        get => _userId;
        set => _userId = value;
    }

    public string UserName
    {
        get => _userName;
        set { if (_userName != value) { _userName = value; _stateUsers.UserName = true; } }
    }

    public string UserSurname
    {
        get => _userSurname;
        set { if (_userSurname != value) { _userSurname = value; _stateUsers.UserSurname = true; } }
    }

    #endregion Properties of Users

    #region AUTO methods

    public override int PrimaryKeyValue { get => this.UserId; set => this.UserId = value; }

    public override List<DatabaseParameter> GetEntityDbParameters(bool isStateCheck = true)
    {
        this.ClearSqlParameters();

        this.AddParam(Field.UserId, DbType.Int32, this.UserId, ParameterDirection.Input, true);

        if (isStateCheck && _stateUsers.Password)
        {
            this.AddParam(Field.Password, DbType.String, this.Password);
        }

        if (isStateCheck && _stateUsers.UserSurname)
        {
            this.AddParam(Field.UserSurname, DbType.String, this.UserSurname);
        }

        if (isStateCheck && _stateUsers.UserCode)
        {
            this.AddParam(Field.UserCode, DbType.String, this.UserCode);
        }

        if (isStateCheck && _stateUsers.UserName)
        {
            this.AddParam(Field.UserName, DbType.String, this.UserName);
        }

        return this.GetSqlParameters();
    }

    public override DataLayerEnumBase GetPrimaryKeyName()
    {
        return PrimaryKey.UserId;
    }

    public override DataLayerEnumBase GetTableName()
    {
        return TableName.Users;
    }

    #endregion AUTO methods

    #region Methods

    public string FullName => this.UserName + ' ' + this.UserSurname;

    #endregion Methods

    #region IUser Members

    public bool DoesHaveSuperUserRole()
    {
        throw new NotImplementedException();
    }

    public int GetDivisionId()
    {
        throw new NotImplementedException();
    }

    public string GetFullName()
    {
        return this.FullName;
    }

    public int GetLaboratoryId()
    {
        throw new NotImplementedException();
    }

    public LanguageEnum GetLanguage()
    {
        throw new NotImplementedException();
    }

    public string GetName()
    {
        return this.UserName;
    }

    public string GetSurname()
    {
        return this.UserSurname;
    }

    public string GetUserCode()
    {
        return this.UserCode;
    }

    public int GetUserId()
    {
        return this.UserId;
    }

    #endregion IUser Members
}

#region State

[Serializable]
public class UsersState : IState
{
    public bool IsActive { get; set; }
    public bool Password { get; set; }
    public bool UserCode { get; set; }
    public bool UserId { get; set; }
    public bool UserName { get; set; }
    public bool UserSurname { get; set; }

    public void ChangeState(bool state)
    {
        Password = state;
        UserSurname = state;
        UserCode = state;
        UserId = state;
        UserName = state;
        IsActive = state;
    }

    public bool IsDirty()
    {
        return UserId || Password || UserSurname || UserCode || UserName || IsActive;
    }

    #endregion State
}