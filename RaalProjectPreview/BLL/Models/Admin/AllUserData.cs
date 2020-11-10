using RaalProjectPreview.DAL.Models.DBModels;
using RaalProjectPreview.Security.Roles;
using System;

namespace RaalProjectPreview.BLL.Models.Admin
{
    public class AllUserData
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public Nullable<double> Discount { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public ClientRole ClientRole { get; set; }
    }
}