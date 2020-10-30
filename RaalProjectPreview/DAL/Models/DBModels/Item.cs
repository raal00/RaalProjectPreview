using System;
using System.ComponentModel.DataAnnotations;

namespace RaalProjectPreview.DAL.Models.DBModels
{
    public class Item
    {
        public int Id { get; set; }
        /// <summary>
        /// X - 0..9
        /// Y - lat letter (Upper case) 
        /// </summary>
        [DisplayFormat(DataFormatString = "XX-XXXX-YYXX", ApplyFormatInEditMode = true)]
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [MaxLength(30)]
        public string Category { get; set; }
    }
}