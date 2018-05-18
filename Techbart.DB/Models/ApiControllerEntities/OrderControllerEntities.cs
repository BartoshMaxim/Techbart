using Techbart.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techbart.DB.Models
{
    public class OrderLoginRequest : IOrderLoginRequest
    {
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public float OrderWeight { get; set; }

        public OrderType OrderType { get; set; }

        public DateTime OrderDate { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
