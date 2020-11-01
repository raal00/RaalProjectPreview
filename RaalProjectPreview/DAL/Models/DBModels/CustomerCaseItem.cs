using System;

namespace RaalProjectPreview.DAL.Models.DBModels
{
    public class CustomerCaseItem
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ItemId { get; set; }
    }
}