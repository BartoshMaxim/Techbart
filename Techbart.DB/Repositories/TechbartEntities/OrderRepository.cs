using Techbart.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Text;

namespace Techbart.DB.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		public bool DeleteOrder(int orderid)
		{
			using (var context = Bakery.Sql())
			{
				return context.Execute(@"
                    DELETE FROM Images
                    WHERE
                        OrderId = @orderid
                ", new
				{
					orderid
				}) != 0;
			}
		}

		public IOrder GetOrder(int orderid)
		{
			using (var context = Bakery.Sql())
			{
				return context.Query<Order>(@"
                    SELECT
                        OrderId
                        ,CustomerId
                        ,ProductId
                        ,OrderWeight
                        ,OrderDate
                        ,CreatedDate
                    FROM
                        Orders
                    WHERE
                        OrderId = @orderid
                ", new
				{
					orderid
				}).FirstOrDefault();
			}
		}

		public FullOrder GetFullOrder(int orderid)
		{
			using (var context = Bakery.Sql())
			{
				return context.Query<FullOrder, Product, Customer, FullOrder>(@"
                    SELECT
                        o.OrderId
                        ,o.OrderWeight
                        ,o.OrderDate
                        ,o.CreatedDate   
                        ,ca.ProductId
                        ,ca.ProductName
                        ,ca.ProductDescription
                        ,ca.ProductPrice
                        ,ca.ImageId
                        ,ca.AddedDate
                        
                        ,c.CustomerId
                        ,c.FirstName
                        ,c.LastName
                        ,c.CreatedDate
                        ,c.Email
                        ,c.CustomerPassword
                        ,c.CustomerPhone
                        ,c.CustomerRole
                    
                        ,c.Address1
                        ,c.Address2
                        ,c.City
                        ,c.Country
                    FROM
                        Orders as o
                            JOIN Customers as c
                            ON c.CustomerId = o.CustomerId
                            JOIN Products as pa
                            ON pa.ProductId = o.ProductId
                    WHERE
                        OrderId = @orderid 
                ", (o, pa, c) =>
				{
					o.Product = pa;
					o.Customer = c;
					return o;
				}, new
				{
					orderid
				}, splitOn: "ProductId, CustomerId"
				).FirstOrDefault();
			}
		}

		public IList<Order> GetOrders()
		{
			using (var context = Bakery.Sql())
			{
				return context.Query<Order>(@"
                    SELECT
                        OrderId
                        ,CustomerId
                        ,ProductId
                        ,OrderWeight
                        ,OrderDate
                        ,CreatedDate
                    FROM
                        Orders").ToList();
			}
		}

		public bool InsertOrder(IOrder order)
		{
			order.OrderId = GetIdForNextOrder();
			order.CreatedDate = DateTime.Now;
			order.OrderType = OrderType.Unconfirmed;

			if (order.OrderId == 0)
			{
				order.OrderId++;
			}

			using (var context = Bakery.Sql())
			{
				return context.Execute(@"
                    INSERT
                        Orders(OrderId, ProductId, CustomerId, OrderWeight, OrderDate, CreatedDate, OrderTypeId)
                    VALUES (@orderid, @productid, @customerid, @orderweight, @orderdate, @createdDate, @ordertypeid)
                ", new
				{
					orderid = order.OrderId,
					productid = order.ProductId,
					customerid = order.CustomerId,
					orderweight = order.OrderWeight,
					orderdate = order.OrderDate,
					createdDate = order.CreatedDate,
					ordertypeid = order.OrderType
				}) != 0;
			}
		}

		public bool UpdateOrder(IOrder updateOrder)
		{
			using (var context = Bakery.Sql())
			{
				return context.Execute(@"
                    UPDATE
                        Orders
                    SET
                        CustomerId   = @customerid
                        ,ProductId   = @Productid
                        ,OrderWeight = @orderweight
                        ,OrderDate   = @orderdate
                        ,CreatedDate = @createddate
                    WHERE
                        OrderId = @orderid
                ", new
				{
					orderid = updateOrder.OrderId,
					customerid = updateOrder.ProductId,
					productid = updateOrder.ProductId,
					orderweight = updateOrder.OrderWeight,
					orderdate = updateOrder.OrderDate,
					createddate = updateOrder.CreatedDate
				}) != 0;
			}
		}

		private int GetIdForNextOrder()
		{
			var orderID = GetCountRows();

			while (IsExists(orderID))
			{
				orderID++;
			}
			return orderID;
		}

		public bool IsExists(int orderid)
		{
			using (var context = Bakery.Sql())
			{
				return context.ExecuteScalar<int>(@"
                SELECT COUNT(OrderId)
                FROM
                    Orders
                WHERE
                    OrderId = @orderid
                ", new
				{
					orderid
				}) != 0;
			}
		}

		private string CreateQuery(IOrder order)
		{
			var query = new StringBuilder();

			if (order.OrderId != 0)
			{
				query.Append($"WHERE OrderId={order.OrderId}");
			}

			if (order.ProductId != 0)
			{
				if (query.Length == 0)
				{
					query.Append("WHERE ");
				}
				else
				{
					query.Append(" AND ");
				}

				query.Append($"ProductId ={order.ProductId}");
			}

			if (order.CustomerId != 0)
			{
				if (query.Length == 0)
				{
					query.Append("WHERE ");
				}
				else
				{
					query.Append(" AND ");
				}

				query.Append($"CustomerId ={order.CustomerId}");
			}

			if (!order.OrderDate.Year.Equals(1))
			{
				if (query.Length == 0)
				{
					query.Append("WHERE ");
				}
				else
				{
					query.Append(" AND ");
				}

				query.Append($"OrderDate ='{order.OrderDate.ToString("yyyy-mm-dd")}'");
			}

			if (order.OrderType != OrderType.All)
			{
				if (query.Length == 0)
				{
					query.Append("WHERE ");
				}
				else
				{
					query.Append(" AND ");
				}

				query.Append($"OrderTypeId ={(int)order.OrderType}");
			}

			return query.ToString();
		}

		public IList<Order> GetOrders(int from, int to, IOrder searchOrder)
		{
			var query = string.Empty;
			if (searchOrder != null)
			{
				query = CreateQuery(searchOrder);
			}

			to = to - from;

			using (var context = Bakery.Sql())
			{
				return context.Query<Order>($@"
                    SELECT
                        OrderId
                        ,CustomerId
                        ,ProductId
                        ,OrderWeight
                        ,OrderDate
                        ,CreatedDate
                    FROM
                        Orders
                    {query}
                    ORDER BY SupplementId DESC
                    OFFSET @from ROWS
                    FETCH NEXT @to ROWS ONLY
                    ", new
				{
					from,
					to
				}).ToList();
			}
		}

		public int GetCountRows(IOrder searchOrder)
		{
			string query = string.Empty;

			if (searchOrder != null)
			{
				query = CreateQuery(searchOrder);
			}
			using (var context = Bakery.Sql())
			{
				return context.ExecuteScalar<int>(@"
                    SELECT COUNT(OrderId)       
                    FROM 
                        Orders
                    " + query);
			}
		}

		public int GetCountRows()
		{
			using (var context = Bakery.Sql())
			{
				return context.ExecuteScalar<int>(@"
                    SELECT COUNT(OrderId)       
                    FROM 
                        Orders");
			}
		}
	}
}