using System;
using System.Collections.Generic;

namespace Techbart.DB.Interfaces
{
	public interface ISupplementRepository : IDisposable
    {
        IList<Supplement> GetSupplements();

        IList<Supplement> GetSupplements(SearchSupplementModel searchSupplement);

        ISupplement GetSupplement(int supplementid);

        bool InsertSupplement(ISupplement supplement);

        bool DeleteSupplement(int supplementid);

        bool UpdateSupplement(ISupplement updateSupplement);

        bool IsExists(int supplementid);

        int Count();

        int Count(ISupplement searchSupplement);
    }
}
