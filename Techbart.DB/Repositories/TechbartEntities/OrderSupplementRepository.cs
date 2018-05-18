using Techbart.DB.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Dapper;

namespace Techbart.DB.Repositories
{
    public class OrderSupplementRepository : IOrderSupplementRepository
    {
        public bool DeleteOrderSupplementReference(IOrderSupplement orderSupplement)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    DELETE FROM OrderSupplements
                    WHERE
                        OrderId       = @orderid
                    AND
                        SupplementId = @supplementid
                ", new
                {
                    orderid = orderSupplement.OrderId,
                    supplementid = orderSupplement.SupplementId
                }) != 0;
            }
        }

        public bool DeleteOrderSupplementReference(int ordersupplementid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    DELETE FROM OrderSupplements
                    WHERE
                        OrderSupplementId = @OrderSupplementId
                ", new
                {
                    OrderSupplementId = ordersupplementid,
                }) != 0;
            }
        }

        public int GetOrderSupplementId(IOrderSupplement orderSupplement)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                    SELECT
                        OrderSupplementId
                    FROM
                        OrderSupplements
                    WHERE
                        OrderId       = @orderid
                    AND
                        SupplementId = @supplementid
                ", new
                {
                    orderid = orderSupplement.OrderId,
                    supplementid = orderSupplement.SupplementId
                });
            }
        }

        public IOrderSupplement GetOrderSupplement(int ordersupplementid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<OrderSupplement>(@"
                    SELECT
                        OrderSupplementId,
                        SupplementId,
                        Orderd
                    FROM
                        OrderSupplements
                    WHERE
                        OrderSupplementId = @ordersupplementid
                ", new
                {
                    ordersupplementid
                }).FirstOrDefault();
            }
        }

        public IList<Supplement> GetSupplements(int orderid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<Supplement>(@"
                    SELECT
                        s.SupplementId
                        ,s.SupplementName
                        ,s.SupplementDescription
                    FROM
                        Supplements as s
                            JOIN OrderSupplements as cs
                            ON cs.SupplementId = s.SupplementId
                            AND cs.OrderId      = @orderid                               
                ", new
                {
                    orderid
                }).ToList(); 
            }
        }

        public bool InsertOrderSupplementReference(IOrderSupplement orderSupplement)
        {
            orderSupplement.OrderSupplementId = GetIdForNextOrderSupplement();

            if (orderSupplement.OrderSupplementId == 0)
            {
                orderSupplement.OrderSupplementId++;
            }

            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    INSERT
                        OrderSupplements (OrderSupplementId, OrderId, SupplementId)
                    VALUES
                        (@ordersupplementid, @orderid, @supplementid)
                    ", new
                {
					ordersupplementid = orderSupplement.OrderSupplementId,
                    orderid = orderSupplement.OrderId,
                    supplementid = orderSupplement.SupplementId
                }) != 0;
            }
        }

        public int GetCountRows()
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                    SELECT COUNT(OrderSupplementId)       
                    FROM 
                        OrderSupplements");
            }
        }

        private int GetIdForNextOrderSupplement()
        {
            var OrderSupplementId = GetCountRows();

            while (IsExists(OrderSupplementId))
            {
                OrderSupplementId++;
            }
            return OrderSupplementId;
        }

        public bool IsExists(int ordersuppmentid)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                SELECT COUNT(OrderSupplementId)
                FROM
                    OrderSupplements
                WHERE
                    OrderSupplementId = @ordersuppmentid
                ", new
                {
                    ordersuppmentid
                }) != 0;
            }
        }
    }
}