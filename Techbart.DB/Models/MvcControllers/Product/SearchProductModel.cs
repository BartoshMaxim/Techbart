using Techbart.DB.Interfaces;
using System;

namespace Techbart.DB
{
	public class SearchProductModel : IPage, IBaseProduct
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public float ProductPrice { get; set; }

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