using System.Collections.Generic;

namespace Techbart.DB.Interfaces
{
	public interface IProductRepository
    {
        IList<Product> GetProducts();

        IList<Product> GetProducts(int from, int to, IBaseProduct searchProduct);

        IProduct GetProduct(int productid);

        int InsertProduct(IProduct product);

        bool DeleteProduct(int productid);

        bool UpdateProduct(IProduct updateProduct);

        bool IsExists(int productid);

        int GetCountRows(IBaseProduct product);

        int GetCountRows();
    }
}
