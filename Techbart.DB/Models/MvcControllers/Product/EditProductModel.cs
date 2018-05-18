using Techbart.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Techbart.DB
{
	public class EditProductModel : IProduct
    {
        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Enter Product Name")]
        [MaxLength(50, ErrorMessage = "Product Name can not contain more than 50 characters")]
        public string ProductName { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Enter Product Description")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Enter Product Price")]
        public float ProductPrice { get; set; }

        public int ImageId { get; set; }

        [Required]
        public DateTime AddedDate { get; set; }

        public IEnumerable<HttpPostedFileBase> Files { get; set; }

        public static explicit operator EditProductModel(Product product)
        {
            return new EditProductModel
            {
                AddedDate = product.AddedDate,
                ProductDescription = product.ProductDescription,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ImageId = product.ImageId
            };
        }

    }
}
