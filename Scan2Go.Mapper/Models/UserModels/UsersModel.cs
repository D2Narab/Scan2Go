﻿using Scan2Go.Mapper.BaseClasses;
using Utility.Bases;
using Utility.Enum;

namespace Scan2Go.Mapper.Models.UserModels;

public class UsersModel : BaseModel, IUser
{
    public bool IsActive { get; set; }
    public string Password { get; set; }
    public string UserCode { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string UserSurname { get; set; }

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
        throw new NotImplementedException();
    }

    public int GetLaboratoryId()
    {
        throw new NotImplementedException();
    }

    public LanguageEnum GetLanguage()
    {
        return LanguageEnum.EN;
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

    #region BaseModel Members

    public override int PkId => UserId;

    #endregion BaseModel Members
}