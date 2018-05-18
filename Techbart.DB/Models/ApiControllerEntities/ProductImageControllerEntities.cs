using Techbart.DB.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Techbart.DB.Models
{
	public class ProductImageLoginRequest : IProductImageLoginRequest
    {
        public int ProductImageId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int ImageId { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
