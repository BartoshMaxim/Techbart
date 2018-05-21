using Techbart.DB.Interfaces;

namespace Bakery.DB.Models.MvcControllers
{
	public class SearchModel : IPage
	{
		public string OrderBy { get; set; }

		public int Rows { get; set; }

		public int Skip { get; set; }

		public int Take { get; set; }

		public int Count { get; set; }

		public bool IsDesc { get; set; }

		public SearchModel(string field)
		{
			Skip = 0;
			OrderBy = field;
			Take = 10;
			IsDesc = true;
		}
	}
}
