using Techbart.DB.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace Techbart.DB.Repositories
{
	public class OrderTypeRepository : IOrderTypeRepository
    {
        public IList<Order> GetOrders(int ordertypeid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<Order>(@"
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
            }
        }

        public IList<OrderType> GetOrderTypes()
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<OrderType>(@"
                    SELECT
                        OrderTypeId
                    FROM
                        OrderTypes
                ").ToList();
            }
        }
    }
}