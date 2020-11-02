using System;

namespace RaalProjectPreview.BLL.Models.Admin
{
    public class OrderData
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string OrderDate { get; set; }
        public string ShipmentDate { get; set; }
        public int OrderNumber { get; set; }
        public string Status { get; set; }
    }
}