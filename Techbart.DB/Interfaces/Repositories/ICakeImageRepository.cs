using System.Collections.Generic;

namespace Techbart.DB.Interfaces
{
	public interface IProductImageRepository
    {
        IList<Image> GetImages(int productid);

        int GetProductImageId(IProductImage productImage);

        bool InsertProductImageReference(IProductImage productImage);

        bool DeleteProductImageReference(IProductImage productImage);

        bool DeleteProductImageReference(int productImageId);

        bool IsExists(int productimageid);

        int GetCountRows();

        bool IsExists(IProductImage productImage);
    }
}
