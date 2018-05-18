namespace Techbart.DB.Interfaces
{
    public interface IImage
    {
        int ImageId { get; set; }

        string ImageName { get; set; }

        string ImagePath { get; set; }
    }
}
