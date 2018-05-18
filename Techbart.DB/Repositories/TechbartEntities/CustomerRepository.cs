using Techbart.DB.Interfaces;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Data;

namespace Techbart.DB.Repositories
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly IDbConnection _context;

		public CustomerRepository()
		{
			_context = Bakery.Sql();
		}

		public bool DeleteCustomer(int customerid)
		{
			return _context.Execute(@"
                    DELETE FROM Customers
                    WHERE
                        CustomerId = @customerid
                ", new
			{
				customerid
			}) != 0;
		}

		public ICustomer GetCustomer(int customerid)
		{
			return _context.Query<Customer>(@"
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
                        CustomerId = @customerid
                ", new
			{
				customerid
			}).FirstOrDefault();
		}


		public int GetCustomerId(string login, string password)
		{
			return _context.ExecuteScalar<int>(@"
                    SELECT
                        CustomerId
                    FROM
                        Customers
                    WHERE
                        Email            = @login
                    AND
                        CustomerPassword = @password
                ", new
			{
				login,
				password = ToMd5(password)
			});
		}

		public ICustomer GetCustomer(string login, string password)
		{
			return _context.Query<Customer>(@"
                    SELECT
                        CustomerId
                        ,FirstName
                        ,LastName
                        ,CreatedDate
                        ,Email
                        ,CustomerPassword
                        ,CustomerPhone
                        ,CustomerRole    = CustomerRoleId
                    
                        ,Address1
                        ,Address2
                        ,City
                        ,Country
                    FROM
                        Customers
                    WHERE
                        Email            = @login
                    AND
                        CustomerPassword = @password
                ", new
			{
				login,
				password = ToMd5(password)
			}).FirstOrDefault();
		}

		public IList<Customer> GetCustomers()
		{
			return _context.Query<Customer>(@"
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
                ").ToList();
		}

		public bool InsertCustomer(ICustomer customer)
		{
			var isExists = IsExists(customer.Email);

			if (!isExists)
			{
				customer.CustomerId = GetIdForNextCustomer();

				customer.CreatedDate = DateTime.Now;

				return _context.Execute(@"
                    INSERT 
                        Customers(CustomerId, FirstName, LastName, CreatedDate, Email, CustomerPassword, CustomerPhone, CustomerRoleId, Address1, Address2, City, Country)
                    VALUES
                        (@customerid, @firstname, @lastname, @createddate, @email, @customerpassword, @customerphone, @customerrole, @address1, @address2, @city, @country)
                ", new
				{
					customerid = customer.CustomerId != 0 ? customer.CustomerId : ++customer.CustomerId,
					firstname = customer.FirstName,
					lastname = customer.LastName,
					createddate = customer.CreatedDate,
					email = customer.Email,
					customerpassword = ToMd5(customer.CustomerPassword),
					customerphone = customer.CustomerPhone,
					customerrole = customer.CustomerRole,

					address1 = customer.Address1,
					address2 = customer.Address2,
					city = customer.City,
					country = customer.Country
				}) != 0;
			}
			else
			{
				return false;
			}
		}

		public bool UpdateCustomer(ICustomer updateCustomer) =>
			_context.Execute(@"
                    UPDATE 
                        Customers
                    SET
                        FirstName         = @firstname
                        ,LastName         = @lastname
                        ,CreatedDate      = @createddate
                        ,CustomerPhone    = @customerphone
                        ,CustomerRoleId   = @customerrole
                        ,Address1         = @address1
                        ,Address2         = @address2
                        ,City             = @city
                        ,Country          = @country
                    WHERE
                        CustomerId        = @customerid
                ", new
			{
				customerid = updateCustomer.CustomerId,
				firstname = updateCustomer.FirstName,
				lastname = updateCustomer.LastName,
				createddate = updateCustomer.CreatedDate,
				email = updateCustomer.Email,
				customerphone = updateCustomer.CustomerPhone,
				customerrole = updateCustomer.CustomerRole,

				address1 = updateCustomer.Address1,
				address2 = updateCustomer.Address1,
				city = updateCustomer.City,
				country = updateCustomer.Country
			}) != 0;


		public bool UpdateCustomerPassword(int customerid, string oldpassword, string newpassword)
		{
			if (IsExists(customerid))
			{
				var customer = GetCustomer(customerid);

				if (customer.CustomerPassword.Equals(ToMd5(oldpassword)))
				{
					return _context.Execute(@"
                    UPDATE 
                        Customers
                    SET
                        CustomerPassword = @customerpassword
                    WHERE
                        CustomerId        = @customerid
                ", new
					{
						customerid,
						customerpassword = ToMd5(newpassword)
					}) != 0;
				}
			}
			return false;
		}

		public IList<Customer> GetCustomers(SearchCustomerModel searchCustomerModel)
		{
			if (!searchCustomerModel.Validate())
			{
				throw new ArgumentException("SearchCustomerModel didn't pass validation");
			}

			return _context.Query<Customer>($@"
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
                    {CreateQuery(searchCustomerModel)}
                    ORDER BY {searchCustomerModel.OrderBy}{(searchCustomerModel.IsDesc ? " DESC" : string.Empty)}
                    OFFSET @skip ROWS
                    FETCH NEXT @take ROWS ONLY
                ", new
			{
				skip = searchCustomerModel.Skip,
				take = searchCustomerModel.Take
			}
			).ToList();
		}

		public int Count() =>
				_context.ExecuteScalar<int>(@"
                    SELECT COUNT(CustomerId)       
                    FROM 
                        Customers");

		public int Count(ICustomerBase customer) =>
				_context.ExecuteScalar<int>(@"
                    SELECT COUNT(CustomerId)       
                    FROM 
                        Customers
                    " + CreateQuery(customer));


		public bool IsExists(string email) =>
			_context.ExecuteScalar<int>(@"
                SELECT COUNT(CustomerId)
                FROM
                    Customers
                WHERE
                    Email = @email
                ", new
			{
				email
			}) != 0;

		public bool IsExists(int customerid) =>
			_context.ExecuteScalar<int>(@"
                SELECT COUNT(CustomerId)
                FROM
                    Customers
                WHERE
                    CustomerId = @customerid
                ", new
			{
				customerid
			}) != 0;

		public bool IsAdmin(ILoginModel loginModel)
		{
			return (RoleType)_context.ExecuteScalar<int>(@"
                    SELECT
                        CustomerRoleId
                    FROM
                        Customers
                    WHERE
                        Email            = @email
                    AND
                        CustomerPassword = @password
                ", new
			{
				email = loginModel.Login,
				password = ToMd5(loginModel.Password)
			}) == RoleType.Admin;
		}

		public void Dispose()
		{
			_context.Dispose();
		}

		private string CreateQuery(ICustomerBase customer)
		{
			if (customer == null)
			{
				return string.Empty;
			}

			var query = new StringBuilder();

			if (customer.CustomerId > 0)
			{
				query.Append($"WHERE CustomerId={customer.CustomerId}");
			}

			if (!string.IsNullOrEmpty(customer.FirstName))
			{
				if (query.Length == 0)
				{
					query.Append("WHERE ");
				}
				else
				{
					query.Append(" AND ");
				}

				query.Append($"FirstName LIKE N'%{customer.FirstName}%'");
			}

			if (!string.IsNullOrEmpty(customer.LastName))
			{
				if (query.Length == 0)
				{
					query.Append("WHERE ");
				}
				else
				{
					query.Append(" AND ");
				}

				query.Append($"LastName LIKE N'%{customer.LastName}%'");
			}

			if (!string.IsNullOrEmpty(customer.Email))
			{
				if (query.Length == 0)
				{
					query.Append("WHERE ");
				}
				else
				{
					query.Append(" AND ");
				}

				query.Append($"Email LIKE '%{customer.Email}%'");
			}

			return query.ToString();
		}

		private string ToMd5(string password)
		{
			StringBuilder hash = new StringBuilder();
			byte[] bytes = new MD5CryptoServiceProvider().ComputeHash(new UTF8Encoding().GetBytes(password));

			for (int i = 0; i < bytes.Length; i++)
			{
				hash.Append(bytes[i].ToString("x2"));
			}
			return hash.ToString();
		}

		private int GetIdForNextCustomer()
		{
			var customerID = Count();

			while (IsExists(customerID))
			{
				customerID++;
			}
			return customerID;
		}
	}
}