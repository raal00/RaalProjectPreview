using RaalProjectPreview.Security.Roles;
using System;
namespace RaalProjectPreview.BLL.Models.Home.Request
{
    public class AuthRequestModel
    {
        public string Name { get; set; }
        public string CustomerCode { get; set; }
        public string Address { get; set; }

        public ClientRole Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}