using RaalProjectPreview.Security.Roles;
using System;
using System.ComponentModel.DataAnnotations;

namespace RaalProjectPreview.BLL.Models.Home.Request
{
    public class AuthRequestModel
    {
        public string Name { get; set; }
        public string CustomerCode { get; set; }
        public string Address { get; set; }

        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}