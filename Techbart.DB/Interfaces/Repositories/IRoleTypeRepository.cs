using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techbart.DB.Interfaces
{
    public interface IRoleTypeRepository
    {
        IList<RoleType> GetRoleTypes();

        IList<Customer> GetCustomers(int roletypeid);

        IList<string> GetRolesDescriptions();
    }
}
