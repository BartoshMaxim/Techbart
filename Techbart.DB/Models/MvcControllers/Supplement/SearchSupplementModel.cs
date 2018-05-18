using Techbart.DB.Interfaces;

namespace Techbart.DB
{
	public class SearchSupplementModel : ISupplement, IPage
    {
        public int SupplementId { get; set; }

        public string SupplementName { get; set; }

        public string SupplementDescription { get; set; }

        public int SupplementPrice { get; set; }

        public float SupplementWeight { get; set; }

        public int Rows { get; set; }

        public int Page { get; set; }

		public string OrderBy { get; set; }

		public int Skip { get; set; }

		public int Take { get; set; }

		public int Count { get; set; }

		public bool IsDesc { get; set; }

		public bool Validate()
		{
			return !string.IsNullOrEmpty(OrderBy) && Skip >= 0 && Take > 1;
		}
	}
}
