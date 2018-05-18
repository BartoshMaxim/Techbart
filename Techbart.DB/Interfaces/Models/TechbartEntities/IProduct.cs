using System;

namespace Techbart.DB.Interfaces
{
	public interface IBaseProduct
	{
		int ProductId { get; set; }

		string ProductName { get; set; }

		float ProductPrice { get; set; }
	}

	public interface IProduct : IBaseProduct
	{
		string ProductDescription { get; set; }

		/// <summary>
		/// Preview image
		/// </summary>
		int ImageId { get; set; }

		DateTime AddedDate { get; set; }
	}
}
