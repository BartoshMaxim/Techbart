using Techbart.DB.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Techbart.DB.Models
{
	public class OrderSupplementLoginRequest : IOrderSupplementLoginRequest
    {
        public int OrderSupplementId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int SupplementId { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
