using Bakery.DB.Models.MvcControllers;
using Techbart.DB.Interfaces;

namespace Techbart.DB
{
	public class SearchSupplementModel : SearchModel, ISupplement
    {
        public int SupplementId { get; set; }

        public string SupplementName { get; set; }

        public string SupplementDescription { get; set; }

        public int SupplementPrice { get; set; }

        public float SupplementWeight { get; set; }

		public SearchSupplementModel() : base("SupplementId") { }

		public bool Validate()
		{
			return !string.IsNullOrEmpty(OrderBy) && Skip >= 0 && Take > 1;
		}
	}
}
