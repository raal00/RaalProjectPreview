using RaalProjectPreview.DAL.Models.DBModels;
using RaalProjectPreview.Security.Roles;
using System;
using System.Data.Entity;
using System.Linq;

namespace RaalProjectPreview.DAL.Repository
{
    public class User_RoleRepository : RepositoryBase<UserRole>
    {
        public User_RoleRepository(DbContext context) : base(context)
        {

        }
        public UserRole GetUserRoleModelById(int id)
        {
            return _DbSet.Where(x => x.Id == id).FirstOrDefault();
        }
        public UserRole UpdateUserRoleModel(UserRole user_role)
        {
            UserRole _updatedUserRole = GetUserRoleModelById(user_role.Id);
            _updatedUserRole.ClientRole = user_role.ClientRole;
            _updatedUserRole.CustomerId = user_role.CustomerId;
            _APPContext.SaveChanges();
            return _updatedUserRole;
        }

        public ClientRole GetRoleByCustomerId(int id)
        {
            return (from role_user in _DbSet
                    where role_user.CustomerId == id
                    select role_user.ClientRole).FirstOrDefault();
        }
        public UserRole GetByCustomerId(int id)
        {
            return (from role_user in _DbSet
                    where role_user.CustomerId == id
                    select role_user).FirstOrDefault();
        }
    }
}