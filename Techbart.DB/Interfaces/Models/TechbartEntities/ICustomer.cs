using System;
using System.ComponentModel.DataAnnotations;

namespace Techbart.DB.Interfaces
{
	public interface ICustomerBase
	{
		int CustomerId { get; set; }

		[Required]
		string FirstName { get; set; }

		[Required]
		string LastName { get; set; }

		[Required]
		[EmailAddress]
		string Email { get; set; }
	}

	public interface ICustomer : ICustomerBase
	{
		DateTime CreatedDate { get; set; }

		[Required]
		string CustomerPassword { get; set; }

		[Required]
		string CustomerPhone { get; set; }

		[Required]
		string Address1 { get; set; }

		string Address2 { get; set; }

		[Required]
		string City { get; set; }

		[Required]
		string Country { get; set; }

		[Required]
		RoleType CustomerRole { get; set; }
	}
}
