using System;
using System.Collections.Generic;

namespace Techbart.DB.Interfaces
{
	public interface IProductRepository : IDisposable
	{
		IList<Product> GetProducts();

		IList<Product> GetProducts(SearchProductModel searchProduct);

		IProduct GetProduct(int productid);

		int InsertProduct(IProduct product);

		bool DeleteProduct(int productid);

		bool UpdateProduct(IProduct updateProduct);

		bool IsExists(int productid);

		int Count(IBaseProduct product);

		int Count();
	}
}
