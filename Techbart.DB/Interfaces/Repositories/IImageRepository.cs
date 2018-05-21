using System;
using System.Collections.Generic;

namespace Techbart.DB.Interfaces
{
	public interface IImageRepository : IDisposable
	{
		IList<Image> GetImages();

		IList<Image> GetImages(SearchImageModel searchImageModel);

		IImage GetImage(int imageid);

		bool InsertImage(IImage image);

		bool DeleteImage(int imageid);

		bool UpdateImage(IImage updateImage);

		bool IsExists(int imageid);

		int Count();

		int Count(IImage image);

		int GetIdForNextImage();
	}
}
