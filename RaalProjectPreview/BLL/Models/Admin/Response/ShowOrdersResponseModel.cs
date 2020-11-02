using RaalProjectPreview.DAL.Models.DBModels;
using System;
using System.Collections.Generic;

namespace RaalProjectPreview.BLL.Models.Admin.Response
{
    public class ShowOrdersResponseModel
    {
        public List<OrderData> Orders { get; set; }
    }
}