using Techbart.DB.Interfaces;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using System.Text;

namespace Techbart.DB.Repositories
{
	public class ProductRepository : IProductRepository
	{
		public bool DeleteProduct(int productid)
		{
			using (var context = Bakery.Sql())
			{
				return context.Execute(@"
                    DELETE FROM Products
                    WHERE
                        ProductId = @productid
                ", new
				{
					productid
				}) != 0;
			}
		}

		public IProduct GetProduct(int productid)
		{
			using (var context = Bakery.Sql())
			{
				return context.Query<Product>(@"
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
			}
		}

		public IList<Product> GetProducts()
		{
			using (var context = Bakery.Sql())
			{
				return context.Query<Product>(@"
                    SELECT
                        ProductId
                        ,ProductName
                        ,ProductDescription
                        ,ProductPrice
                        ,ImageId
                        ,AddedDate
                    FROM
                        Products").ToList();
			}
		}

		public int InsertProduct(IProduct product)
		{
			product.ProductId = GetIdForNextProduct();
			product.AddedDate = DateTime.Now;

			if (product.ProductId == 0)
			{
				product.ProductId++;
			}

			using (var context = Bakery.Sql())
			{
				return context.Execute(@"
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
		}

		public bool UpdateProduct(IProduct updateProduct)
		{
			using (var context = Bakery.Sql())
			{
				return context.Execute(@"
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

		public IList<Product> GetProducts(int from, int to, IBaseProduct searchProduct)
		{
			var query = string.Empty;
			if (searchProduct != null)
			{
				query = CreateQuery(searchProduct);
			}

			to = to - from;

			using (var context = Bakery.Sql())
			{
				return context.Query<Product>($@"
                   SELECT
                        ProductId
                        ,ProductName
                        ,ProductDescription
                        ,ProductPrice
                        ,ImageId
                        ,AddedDate
                    FROM
                        Products
                    {query}
                    ORDER BY ProductId DESC
                    OFFSET @from ROWS
                    FETCH NEXT @to ROWS ONLY
                ", new
				{
					from,
					to
				}
				).ToList();
			}
		}

		public int GetCountRows(IBaseProduct searchProduct)
		{
			string query = string.Empty;

			if (searchProduct != null)
			{
				query = CreateQuery(searchProduct);
			}

			using (var context = Bakery.Sql())
			{
				return context.ExecuteScalar<int>(@"
                    SELECT COUNT(ProductId)       
                    FROM 
                        Products
                    " + query);
			}
		}

		public int GetCountRows()
		{
			using (var context = Bakery.Sql())
			{
				return context.ExecuteScalar<int>(@"
                    SELECT COUNT(ProductId)       
                    FROM 
                        Products");
			}
		}

		private int GetIdForNextProduct()
		{
			var productID = GetCountRows();

			while (IsExists(productID))
			{
				productID++;
			}
			return productID;
		}

		public bool IsExists(int productid)
		{
			using (var context = Bakery.Sql())
			{
				return context.ExecuteScalar<int>(@"
                SELECT COUNT(ProductId)
                FROM
                    Products
                WHERE
                    ProductId = @productid
                ", new
				{
					productid
				}) != 0;
			}
		}
	}
}