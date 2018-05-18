using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techbart.DB.Interfaces
{
    public interface IOrderSupplementRepository
    {
        IList<Supplement> GetSupplements(int orderid);

        int GetOrderSupplementId(IOrderSupplement orderSupplement);

        bool InsertOrderSupplementReference(IOrderSupplement orderSupplement);

        bool DeleteOrderSupplementReference(IOrderSupplement orderSupplement);

        bool DeleteOrderSupplementReference(int ordersupplementid);

        IOrderSupplement GetOrderSupplement(int ordersupplementid);

        bool IsExists(int ordersuppmentid);

        int GetCountRows();
    }
}
