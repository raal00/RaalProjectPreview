using System;

namespace RaalProjectPreview.DAL.Models.DBModels
{
    public class AuthUserData
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Login { get; set; } // unique login
        public string PasswordHash { get; set; }
    }
}