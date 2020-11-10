using RaalProjectPreview.DAL.Models.DBModels;
using RaalProjectPreview.Security.Roles;
using System;


namespace RaalProjectPreview.BLL.Models.Home.Response
{
    public class LoginResponseModel : BaseResponse
    {
        public ClientRole Role { get; set; }
    }
}