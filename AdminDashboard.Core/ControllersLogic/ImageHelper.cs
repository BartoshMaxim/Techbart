using Techbart.DB;
using Techbart.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AdminDashboard.Core.ControllersLogic
{
    public static class ImageHelper
    {
        public static int UploadImage(IUploadImageModel imageModel, IImageRepository imageController, HttpServerUtilityBase server)
        {
            var uploadpath = "~/Images/Upload";

            var imageid = imageController.GetIdForNextImage();

            if (imageid == 0)
            {
                imageid++;
            }

            var ext = Path.GetExtension(imageModel.ImageFile.FileName);

            var physicalImagePath = Path.Combine(server.MapPath(uploadpath), imageid + ext);

            var imagepath = uploadpath + "/" + imageid + ext;

            var image = new Image
            {
                ImageId = imageid,
                ImageName = imageModel.ImageName,
                ImagePath = imagepath
            };

            imageModel.ImageFile.SaveAs(physicalImagePath);

            var result = imageController.InsertImage(image);

            if(result == false)
            {
                return 0;
            }

            return imageid;
        }
    }
}
