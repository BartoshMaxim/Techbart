using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Techbart.DB.Interfaces;

namespace Techbart.DB.Repositories
{
	public class RoleTypeRepository : IRoleTypeRepository
	{
		private readonly IDbConnection _context;

		public RoleTypeRepository()
		{
			_context = Bakery.Sql();
		}

		public IList<Customer> GetCustomers(int roletypeid) =>
		_context.Query<Customer>(@"
                    SELECT
                        CustomerId
                        ,FirstName
                        ,LastName
                        ,CreatedDate
                        ,Email
                        ,CustomerPassword
                        ,CustomerPhone
                        ,CustomerRole = CustomerRoleId
                    
                        ,Address1
                        ,Address2
                        ,City
                        ,Country
                    FROM
                        Customers
                    WHERE
                        CustomerRoleId = @roletypeid
                ", new
		{
			roletypeid
		}).ToList();

		public IList<RoleType> GetRoleTypes() =>
			_context.Query<RoleType>(@"
                    SELECT
                        CustomerRoleId
                    FROM
                        CustomerRoles
                ").ToList();

		public IList<string> GetRolesDescriptions() =>
				_context.Query<string>(@"
                    SELECT
                        RoleName
                    FROM
                        CustomerRoles
                ").ToList();

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}