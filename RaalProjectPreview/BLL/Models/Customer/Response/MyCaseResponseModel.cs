using RaalProjectPreview.BLL.Models.Home.Response;
using RaalProjectPreview.DAL.Models.DBModels;
using System;
using System.Collections.Generic;

namespace RaalProjectPreview.BLL.Models.Customer.Response
{
    public class MyCaseResponseModel : BaseResponse
    {
        public List<Item> CustomerCase { get; set; }
    }
}