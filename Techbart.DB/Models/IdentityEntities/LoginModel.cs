using Techbart.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Techbart.DB
{
    /// <summary>
    /// Login Model - use for Authentication
    /// </summary>
    public class LoginModel : ILoginModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Not correct email")]
        public string Login { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password has to longer than 6 symbols")]
        [MaxLength(256, ErrorMessage = "Password has to smaller than 30 symbols")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}