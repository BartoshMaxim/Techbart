using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techbart.DB.Interfaces
{
    public interface ILoginModel
    {
        [Required]
        string Login { get; set; }

        [Required]
        string Password { get; set; }
    }
}
