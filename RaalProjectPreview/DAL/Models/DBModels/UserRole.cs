using RaalProjectPreview.Security.Roles;
using System;

namespace RaalProjectPreview.DAL.Models.DBModels
{
    public class UserRole
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public ClientRole ClientRole { get; set; }
    }
}