using RaalProjectPreview.BLL.Models.Enums;
using RaalProjectPreview.BLL.Models.Home.Response;
using RaalProjectPreview.Security.Roles;
using System;
namespace RaalProjectPreview.BLL.Models.Home.ServiceModels.Response
{
    public class ServiceLoginResponse : BaseResponse
    {
        public string Name { get; set; } // response user name
        public ClientRole Role { get; set; } // user role
    }
}