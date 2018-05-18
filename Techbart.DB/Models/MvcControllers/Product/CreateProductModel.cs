using Techbart.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Techbart.DB
{
	public class CreateProductModel : IProduct
    {
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

        public DateTime AddedDate { get; set; }

        public IEnumerable<HttpPostedFileBase> Files { get; set; }
    }
}