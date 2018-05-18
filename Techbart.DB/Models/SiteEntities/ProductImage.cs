using Techbart.DB.Interfaces;

namespace Techbart.DB
{
    public class ProductImage : IProductImage
    {
        public int ProductImageId { get; set; }

        public int ProductId { get; set; }

        public int ImageId { get; set; }
    }
}
