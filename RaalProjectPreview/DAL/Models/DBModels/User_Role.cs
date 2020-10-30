using RaalProjectPreview.Security.Roles;
using System;

namespace RaalProjectPreview.DAL.Models.DBModels
{
    public class User_Role
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public ClientRole ClientRole { get; set; }
    }
}