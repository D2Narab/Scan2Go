using DataLayer.Base.DatabaseFactory;
using DataLayer.Base.GeneralDataLayer;
using Scan2Go.DataLayer.BaseClasses.DataLayersBases;
using Scan2Go.Entity.Users;

namespace Scan2Go.DataLayer.UsersDataLayer
{
    internal class UsersDMLOperations : Scan2GoDataLayerBase
    {
        public UsersDMLOperations(DataLayerBase dataLayerBase) : base(dataLayerBase)
        {
        }

        public void DeleteUser(Users user)
        {
            new GeneralDMLOperations(this).DeleteEntity(user);
        }

        internal void SaveUsers(Users users)
        {
            new GeneralDMLOperations(this).SaveEntity(users);
        }
    }
}