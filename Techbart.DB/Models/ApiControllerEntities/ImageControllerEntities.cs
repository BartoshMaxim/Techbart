using Techbart.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techbart.DB.Models
{
    public class ImageLoginRequest : IImageLoginRequest
    {
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
    }
}
