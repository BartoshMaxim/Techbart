using Techbart.DB.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Techbart.DB.Repositories
{
	public class OrderTypeRepository : IOrderTypeRepository
	{
		private readonly IDbConnection _context;

		public OrderTypeRepository()
		{
			_context = Bakery.Sql();
		}

		public IList<Order> GetOrders(int ordertypeid) =>
			_context.Query<Order>(@"
                    SELECT
                        OrderId,
                        ProductId,
                        OrderTypeId,
                        CustomerId,
                        OrderWeight,
                        OrderDate,
                        OrderType = OrderTypeId
                    FROM
                        Orders
                    WHERE
                        OrderTypeId = @ordertypeid
                ", new
			{
				ordertypeid
			}).ToList();

		public IList<OrderType> GetOrderTypes() =>
			_context.Query<OrderType>(@"
                    SELECT
                        OrderTypeId
                    FROM
                        OrderTypes
                ").ToList();

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}