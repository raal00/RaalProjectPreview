using System;
using System.ComponentModel.DataAnnotations;

namespace RaalProjectPreview.DAL.Models.DBModels
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// XXXX - code 
        /// YYYY - register date
        /// </summary>
        [DisplayFormat(DataFormatString = "XXXX-YYYY", ApplyFormatInEditMode = true)]
        public string Code { get; set; }
        public string Address { get; set; }
        public Nullable<double> Discount { get; set; }
    }
}