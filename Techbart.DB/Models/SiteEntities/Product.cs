using Techbart.DB.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Techbart.DB
{
	public class Product : IProduct
    {
        public Product()
        {
            
        }

        public Product(Product product)
        {
            ProductId = product.ProductId;
            ProductName = product.ProductName;
            ProductDescription = product.ProductDescription;
            ProductPrice = product.ProductPrice;
            ImageId = product.ImageId;
            AddedDate = product.AddedDate;
        }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public float ProductPrice { get; set; }

        /// <summary>
        /// Preview image
        /// </summary>
        [Display(Name = "Title Image")]
        public int ImageId { get; set; }

        public DateTime AddedDate { get; set; }
    }
}
