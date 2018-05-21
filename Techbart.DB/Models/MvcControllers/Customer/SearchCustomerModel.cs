using Bakery.DB.Models.MvcControllers;
using Techbart.DB.Interfaces;

namespace Techbart.DB
{
	public class SearchCustomerModel : SearchModel, ICustomerBase
	{
		public int CustomerId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public SearchCustomerModel() : base("CustomerId") { }

		public bool Validate()
		{
			return !string.IsNullOrEmpty(OrderBy) && Skip >= 0 && Take > 1;
		}
	}
}