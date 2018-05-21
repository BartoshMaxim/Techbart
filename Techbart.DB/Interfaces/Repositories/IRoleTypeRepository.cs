using System;
using System.Collections.Generic;

namespace Techbart.DB.Interfaces
{
	public interface IRoleTypeRepository : IDisposable
    {
        IList<RoleType> GetRoleTypes();

        IList<Customer> GetCustomers(int roletypeid);

        IList<string> GetRolesDescriptions();
    }
}
