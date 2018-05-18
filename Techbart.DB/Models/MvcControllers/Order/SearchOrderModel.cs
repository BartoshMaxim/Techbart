using Techbart.DB.Interfaces;
using System;

namespace Techbart.DB
{
	public class SearchOrderModel : IOrder, IPage
	{
		public int OrderId { get; set; }

		public int ProductId { get; set; }

		public int CustomerId { get; set; }

		public float OrderWeight { get; set; }

		public OrderType OrderType { get; set; }

		public DateTime OrderDate { get; set; }

		public DateTime CreatedDate { get; set; }

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