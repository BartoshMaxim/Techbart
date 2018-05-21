using Techbart.DB.Interfaces;
using System;
using Bakery.DB.Models.MvcControllers;

namespace Techbart.DB
{
	public class SearchOrderModel : SearchModel,IOrder
	{
		public int OrderId { get; set; }

		public int ProductId { get; set; }

		public int CustomerId { get; set; }

		public float OrderWeight { get; set; }

		public OrderType OrderType { get; set; }

		public DateTime OrderDate { get; set; }

		public DateTime CreatedDate { get; set; }

		public SearchOrderModel() : base("OrderId") { }

		public bool Validate()
		{
			return !string.IsNullOrEmpty(OrderBy) && Skip >= 0 && Take > 1;
		}
	}
}