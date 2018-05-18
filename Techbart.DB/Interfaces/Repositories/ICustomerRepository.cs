using System;
using System.Collections.Generic;

namespace Techbart.DB.Interfaces
{
	public interface ICustomerRepository : IDisposable
	{
		IList<Customer> GetCustomers();

		IList<Customer> GetCustomers(SearchCustomerModel searchCustomerModel);

		ICustomer GetCustomer(int customerid);

		ICustomer GetCustomer(string login, string password);

		int GetCustomerId(string login, string password);

		bool InsertCustomer(ICustomer customer);

		bool DeleteCustomer(int customerid);

		bool UpdateCustomer(ICustomer updateCustomer);

		int Count();

		int Count(ICustomerBase customer);

		bool IsExists(string email);

		bool IsExists(int id);

		bool IsAdmin(ILoginModel loginModel);
	}
}
