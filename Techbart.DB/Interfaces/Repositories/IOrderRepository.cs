using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techbart.DB.Interfaces
{
    public interface IOrderRepository
    {
        IList<Order> GetOrders();

        IList<Order> GetOrders(int from, int to, IOrder searchOrder);

        IOrder GetOrder(int orderid);

        FullOrder GetFullOrder(int orderid);

        bool InsertOrder(IOrder order);

        bool DeleteOrder(int orderid);

        bool UpdateOrder(IOrder updateOrder);

        int GetCountRows(IOrder searchOrder);

        int GetCountRows();
    }
}
