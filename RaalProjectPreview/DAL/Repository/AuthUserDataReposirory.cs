using RaalProjectPreview.DAL.Models.DBModels;
using System;
using System.Linq;

namespace RaalProjectPreview.DAL.Repository
{
    public class AuthUserDataReposirory : RepositoryBase<AuthUserData>
    {
        public AuthUserData GetAuthUserDataById(int id)
        {
            return _DbSet.Where(x => x.Id == id).FirstOrDefault();
        }
        public AuthUserData UpdateAuthUserData(AuthUserData authData)
        {
            AuthUserData _updatedAuthUserData = GetAuthUserDataById(authData.Id);
            _updatedAuthUserData.Login = authData.Login;
            _updatedAuthUserData.PasswordHash = authData.PasswordHash;
            _updatedAuthUserData.CustomerId = authData.CustomerId;
            _APPContext.SaveChanges();
            return _updatedAuthUserData;
        }

        public AuthUserData GetUserByLoginAndPasswordHash(AuthUserData userData)
        {
            return (from data in _DbSet 
                    where data.Login == userData.Login && data.PasswordHash == userData.PasswordHash 
                    select data).FirstOrDefault();
        }
    }
}