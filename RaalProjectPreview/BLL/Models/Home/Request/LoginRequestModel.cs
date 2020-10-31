using System;
using System.ComponentModel.DataAnnotations;

namespace RaalProjectPreview.BLL.Models.Home.Request
{
    public class LoginRequestModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}