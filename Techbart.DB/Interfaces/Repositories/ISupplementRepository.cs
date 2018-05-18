using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techbart.DB.Interfaces
{
    public interface ISupplementRepository
    {
        IList<Supplement> GetSupplements();

        IList<Supplement> GetSupplements(int from, int to, ISupplement searchSupplement);

        ISupplement GetSupplement(int supplementid);

        bool InsertSupplement(ISupplement supplement);

        bool DeleteSupplement(int supplementid);

        bool UpdateSupplement(ISupplement updateSupplement);

        bool IsExists(int supplementid);

        int GetCountRows();

        int GetCountRows(ISupplement searchSupplement);
    }
}
