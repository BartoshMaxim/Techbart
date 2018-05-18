using Techbart.DB.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Techbart.DB.Models
{
	public class ProductLoginRequest : IProductLoginRequest
    {
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public float ProductPrice { get; set; }

        public int ImageId { get; set; }
        
        public DateTime AddedDate { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
