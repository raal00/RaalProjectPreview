using RaalProjectPreview.DAL.Models.DBModels;
using System;

namespace RaalProjectPreview.BLL.Models.Admin
{
    public class AllUserData
    {
        public Customer Customer { get; set; }
        public AuthUserData AuthUserData { get; set; }
        public UserRole UserRole { get; set; }
    }
}