using Techbart.DB.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Techbart.DB
{
    public class Image : IImage
    {
        public int ImageId { get; set; }

        [Required]
        [Display(Name ="Image Name")]
        public string ImageName { get; set; }

        [Display(Name = "Image Path")]
        public string ImagePath { get; set; }
    }
}
