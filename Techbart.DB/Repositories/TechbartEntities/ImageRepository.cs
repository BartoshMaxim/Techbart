using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Techbart.DB.Interfaces;

namespace Techbart.DB.Repositories
{
	public class ImageRepository : IImageRepository, IDisposable
	{
		private readonly IDbConnection _context;

		public ImageRepository()
		{
			_context = Bakery.Sql();
		}

		public bool DeleteImage(int imageid) =>
			_context.Execute(@"
                    DELETE FROM Images
                    WHERE
                        ImageId = @imageid
                ", new
			{
				imageid
			}) != 0;

		public IImage GetImage(int imageid) =>
			_context.Query<Image>(@"
                    SELECT
                        ImageId
                        ,ImageName
                        ,ImagePath
                    FROM
                        Images
                    WHERE
                        ImageId = @imageid
                ", new
			{
				imageid
			}).FirstOrDefault();

		public IList<Image> GetImages() =>
			_context.Query<Image>(@"
                    SELECT
                        ImageId
                        ,ImageName
                        ,ImagePath
                    FROM
                        Images
                ").ToList();

		public bool InsertImage(IImage image)
		{
			image.ImageId = GetIdForNextImage();

			if (image.ImageId == 0)
			{
				image.ImageId++;
			}

			return _context.Execute(@"
                    INSERT 
                        Images(ImageId, ImageName, ImagePath)
                    VALUES
                        (@imageid, @imagename, @imagepath)
                ", new
			{
				imageid = image.ImageId,
				imagename = image.ImageName,
				imagepath = image.ImagePath
			}) != 0;
		}

		public bool UpdateImage(IImage updateImage) =>
			_context.Execute(@"
                    UPDATE
                        Images
                    SET
                        ImageName = @imagename,
                        ImagePath = @imagepath
                    WHERE 
                        ImageId   = @imageid
                ", new
			{
				imageid = updateImage.ImageId,
				imagename = updateImage.ImageName,
				imagepath = updateImage.ImagePath
			}) != 0;

		public IList<Image> GetImages(SearchImageModel searchImageModel)
		{
			if (!searchImageModel.Validate())
			{
				throw new ArgumentException("SearchImageModel didn't pass validation");
			}

			return _context.Query<Image>($@"
                    SELECT
                        ImageId
                        ,ImageName
                        ,ImagePath
                    FROM
                        Images
                    {CreateQuery(searchImageModel)}
                   ORDER BY {searchImageModel.OrderBy}{(searchImageModel.IsDesc ? " DESC" : string.Empty)}
                    OFFSET @skip ROWS
                    FETCH NEXT @take ROWS ONLY
                ", new
			{
				skip = searchImageModel.Skip,
				take = searchImageModel.Take
			}
				).ToList();
		}

		public int GetCountRows() =>
			_context.ExecuteScalar<int>(@"
                    SELECT COUNT(ImageId)       
                    FROM 
                        Images");

		public int GetCountRows(IImage image)
		{
			var query = string.Empty;
			if (image != null)
			{
				query = CreateQuery(image);
			}

			return _context.ExecuteScalar<int>(@"
                    SELECT COUNT(ImageId)       
                    FROM 
                        Images
                    " + query);
		}

		private string CreateQuery(IImage image)
		{
			if (image == null)
			{
				return string.Empty;
			}

			var query = new StringBuilder();

			if (image.ImageId > 0)
			{
				query.Append($"WHERE ImageId={image.ImageId}");
			}

			if (!string.IsNullOrEmpty(image.ImageName))
			{
				if (query.Length == 0)
				{
					query.Append("WHERE ");
				}
				else
				{
					query.Append(" AND ");
				}

				query.Append($"ImageName LIKE N'%{image.ImageName}%'");
			}

			if (!string.IsNullOrEmpty(image.ImagePath))
			{
				if (query.Length == 0)
				{
					query.Append("WHERE ");
				}
				else
				{
					query.Append(" AND ");
				}

				query.Append($"ImagePath LIKE N'%{image.ImagePath}%'");
			}

			return query.ToString();
		}

		public int GetIdForNextImage()
		{
			var imageID = GetCountRows();

			while (IsExists(imageID))
			{
				imageID++;
			}
			return imageID;
		}

		public bool IsExists(int imageid) =>
			_context.ExecuteScalar<int>(@"
                SELECT COUNT(ImageId)
                FROM
                    Images
                WHERE
                    ImageId = @imageid
                ", new
			{
				imageid
			}) != 0;

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}