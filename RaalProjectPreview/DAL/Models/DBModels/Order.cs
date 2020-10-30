using System;

namespace RaalProjectPreview.DAL.Models.DBModels
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipmentDate { get; set; }
        public int OrderNumber { get; set; }
        public string Status { get; set; }
    }
}