using Bakery.DB.Models.MvcControllers;
using Techbart.DB.Interfaces;

namespace Techbart.DB
{
	public class SearchProductModel : SearchModel, IBaseProduct
	{
		public int ProductId { get; set; }

		public string ProductName { get; set; }

		public float ProductPrice { get; set; }

		public SearchProductModel() : base("ProductId") { }

		public bool Validate()
		{
			return !string.IsNullOrEmpty(OrderBy) && Skip >= 0 && Take > 1;
		}
	}
}