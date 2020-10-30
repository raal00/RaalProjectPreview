using RaalProjectPreview.BLL.Models.Enums;
using System;

namespace RaalProjectPreview.BLL.Models.Home.Response
{
    public class BaseResponse
    {
        public ResponseStatus responseStatus { get; set; }
        public string Message { get; set; }
    }
}