using System;

namespace RaalProjectPreview.DAL.Models.DBModels
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int ItemsCount { get; set; }
        public decimal ItemPrice { get; set; }
    }
}