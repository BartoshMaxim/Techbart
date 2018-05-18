using Techbart.DB.Interfaces;

namespace Techbart.DB
{
	public class SearchCustomerModel : IPage, ICustomerBase
	{
		public int CustomerId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public string OrderBy { get; set; }

		public int Rows { get; set; }

		public int Skip { get; set; }

		public int Take { get; set; }

		public int Count { get; set; }

		public bool IsDesc { get; set; }

		public SearchCustomerModel()
		{
			Skip = 0;
			OrderBy = "CustomerId";
			Take = 25;
			IsDesc = true;
		}

		public bool Validate()
		{
			return !string.IsNullOrEmpty(OrderBy) && Skip >= 0 && Take > 1;
		}
	}
}