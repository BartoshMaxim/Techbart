using System.Web;

namespace Techbart.DB.Interfaces
{
    public interface IUploadImageModel
    {
        string ImageName { get; set; }

        HttpPostedFileBase ImageFile { get; set; }
    }
}
