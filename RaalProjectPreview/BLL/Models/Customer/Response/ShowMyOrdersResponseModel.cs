using RaalProjectPreview.BLL.Models.Admin;
using RaalProjectPreview.BLL.Models.Home.Response;
using RaalProjectPreview.DAL.Models.DBModels;
using System;
using System.Collections.Generic;

namespace RaalProjectPreview.BLL.Models.Customer.Response
{
    public class ShowMyOrdersResponseModel : BaseResponse
    {
        public List<OrderData> Orders { get; set; }
    }
}