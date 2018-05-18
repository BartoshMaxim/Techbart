using Techbart.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techbart.DB.Models.ApiControllerEntities
{
    public class SupplementLoginRequest : ISupplementLoginRequest
    {
        public int SupplementId { get; set; }

        [Required]
        public string SupplementName { get; set; }

        [Required]
        public string SupplementDescription { get; set; }

        [Required]
        public int SupplementPrice { get; set; }

        [Required]
        public string Login { get; set; }

        public float SupplementWeight { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
