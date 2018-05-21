using Techbart.DB.Interfaces;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using System.Text;
using System.Data;

namespace Techbart.DB.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly IDbConnection _context;

		public ProductRepository()
		{
			_context = Bakery.Sql();
		}

		public bool DeleteProduct(int productid) =>
			_context.Execute(@"
                    DELETE FROM Products
                    WHERE
                        ProductId = @productid
                ", new
			{
				productid
			}) != 0;

		public IProduct GetProduct(int productid) =>
			_context.Query<Product>(@"
                    SELECT
                        ProductId
                        ,ProductName
                        ,ProductDescription
                        ,ProductPrice
                        ,ImageId
                        ,AddedDate
                    FROM
                        Products
                    WHERE
                        ProductId = @productid
                ", new
			{
				productid
			}).FirstOrDefault();

		public IList<Product> GetProducts() =>
			_context.Query<Product>(@"
                    SELECT
                        ProductId
                        ,ProductName
                        ,ProductDescription
                        ,ProductPrice
                        ,ImageId
                        ,AddedDate
                    FROM
                        Products").ToList();

		public int InsertProduct(IProduct product)
		{
			product.ProductId = GetIdForNextProduct();
			product.AddedDate = DateTime.Now;

			if (product.ProductId == 0)
			{
				product.ProductId++;
			}

			return _context.Execute(@"
                    INSERT
                        Products (ProductId, ProductName, ProductDescription, ProductPrice, ImageId, AddedDate)
                    VALUES
                        (@productid, @productname, @productdescription, @productprice, @imageid, @addeddate)
                ", new
			{
				productid = product.ProductId,
				productname = product.ProductName,
				productdescription = product.ProductDescription,
				productprice = product.ProductPrice,
				imageid = product.ImageId,
				addeddate = product.AddedDate
			}) != 0 ? product.ProductId : 0;
		}

		public bool UpdateProduct(IProduct updateProduct)
		{
			return _context.Execute(@"
                    UPDATE 
                        Products
                    SET
                        ProductName         = @productname
                        ,ProductDescription = @productdescription
                        ,ProductPrice       = @productprice
                        ,ImageId			= @imageid
                        ,AddedDate			= @addeddate
                    WHERE
                        ProductId = @productid
                ", new
			{
				productid = updateProduct.ProductId,
				productname = updateProduct.ProductName,
				productdescription = updateProduct.ProductDescription,
				productprice = updateProduct.ProductPrice,
				imageid = updateProduct.ImageId,
				addeddate = updateProduct.AddedDate
			}) != 0;
		}

		private string CreateQuery(IBaseProduct product)
		{
			var query = new StringBuilder();

			if (product.ProductId != 0)
			{
				query.Append($"WHERE ProductId={product.ProductId}");
			}

			if (product.ProductName != null && !product.ProductName.Equals(string.Empty))
			{
				if (query.Length == 0)
				{
					query.Append("WHERE ");
				}
				else
				{
					query.Append(" AND ");
				}

				query.Append($"ProductName LIKE N'%{product.ProductName}%'");
			}

			if (product.ProductPrice != 0)
			{
				if (query.Length == 0)
				{
					query.Append("WHERE ");
				}
				else
				{
					query.Append(" AND ");
				}

				query.Append($"ProductPrice={product.ProductPrice}");
			}

			return query.ToString();
		}

		public IList<Product> GetProducts(SearchProductModel searchProduct)
		{
			if (!searchProduct.Validate())
			{
				throw new ArgumentException("SearchProductModel didn't pass validation");
			}

			return _context.Query<Product>($@"
                   SELECT
                        ProductId
                        ,ProductName
                        ,ProductDescription
                        ,ProductPrice
                        ,ImageId
                        ,AddedDate
                    FROM
                        Products
                    {CreateQuery(searchProduct)}
                   ORDER BY {searchProduct.OrderBy}{(searchProduct.IsDesc ? " DESC" : string.Empty)}
                    OFFSET @skip ROWS
                    FETCH NEXT @take ROWS ONLY
                ", new
			{
				skip = searchProduct.Skip,
				take = searchProduct.Take
			}
				).ToList();
		}

		public int Count(IBaseProduct searchProduct)
		{
			string query = string.Empty;

			if (searchProduct != null)
			{
				query = CreateQuery(searchProduct);
			}

			return _context.ExecuteScalar<int>(@"
                    SELECT COUNT(ProductId)       
                    FROM 
                        Products
                    " + query);
		}

		public int Count() =>
			_context.ExecuteScalar<int>(@"
                    SELECT COUNT(ProductId)       
                    FROM 
                        Products");

		private int GetIdForNextProduct()
		{
			var productID = Count();

			while (IsExists(productID))
			{
				productID++;
			}
			return productID;
		}

		public bool IsExists(int productid) =>
			_context.ExecuteScalar<int>(@"
                SELECT COUNT(ProductId)
                FROM
                    Products
                WHERE
                    ProductId = @productid
                ", new
			{
				productid
			}) != 0;

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}