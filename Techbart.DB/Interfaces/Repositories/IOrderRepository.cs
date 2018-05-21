using System;
using System.Collections.Generic;

namespace Techbart.DB.Interfaces
{
	public interface IOrderRepository : IDisposable
	{
        IList<Order> GetOrders();

        IList<Order> GetOrders(SearchOrderModel searchOrder);

        IOrder GetOrder(int orderid);

        FullOrder GetFullOrder(int orderid);

        bool InsertOrder(IOrder order);

        bool DeleteOrder(int orderid);

        bool UpdateOrder(IOrder updateOrder);

        int Count(IOrder searchOrder);

        int Count();
    }
}
