using System;
using System.Collections.Generic;

namespace Techbart.DB.Interfaces
{
	public interface IOrderSupplementRepository : IDisposable
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
