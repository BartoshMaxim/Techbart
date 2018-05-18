using Techbart.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Techbart.DB
{
    public class UploadImageModel: IUploadImageModel
    {
        [Required(ErrorMessage = "Please, write image name, it will use in alt's!")]
        [Display(Name = "Image Name")]
        public virtual string ImageName { get; set; }

        [Required(ErrorMessage = "Please, choose image file!")]
        public virtual HttpPostedFileBase ImageFile { get; set; }
    }
}