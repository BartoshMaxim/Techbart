namespace Techbart.DB.Interfaces
{
	public interface IProductImage
    {
        int ProductImageId { get; set; }

        int ProductId { get; set; }

        int ImageId { get; set; }
    }
}
