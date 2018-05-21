using Bakery.DB.Models.MvcControllers;
using Techbart.DB.Interfaces;

namespace Techbart.DB
{
	public class SearchImageModel : SearchModel, IImage
	{
		public int ImageId { get; set; }

		public string ImageName { get; set; }

		public string ImagePath { get; set; }

		public SearchImageModel() : base("ImageId") { }

		public bool Validate()
		{
			return !string.IsNullOrEmpty(OrderBy) && Skip >= 0 && Take > 1;
		}
	}
}