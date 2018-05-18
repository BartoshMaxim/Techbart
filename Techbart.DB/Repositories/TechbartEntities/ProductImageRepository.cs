using Techbart.DB.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Dapper;

namespace Techbart.DB.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        public bool DeleteProductImageReference(IProductImage productImage)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    DELETE FROM ProductsImages
                    WHERE
                        ProductId   = @productid
                    AND
                        ImageId		= @imageid
                ", new
                {
                    productid = productImage.ProductId,
                    imageid = productImage.ImageId
                }) != 0;
            }
        }

        public bool DeleteProductImageReference(int productImageId)
        {
            using (var context = Bakery.Sql())
            {
                return context.Execute(@"
                    DELETE FROM ProductsImages
                    WHERE
                        ProductImageId = @productImageId
                ", new
                {
                    productImageId
                }) != 0;
            }
        }

        public int GetProductImageId(IProductImage productImage)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                    SELECT
                        ProductImageId
                    FROM
                        ProductsImages
                    WHERE
                        ProductId   = @productid
                    AND
                        ImageId		= @imageid
                ", new
                {
                    productid = productImage.ProductId,
                    imageid = productImage.ImageId
                });
            }
        }

        public IList<Image> GetImages(int productid)
        {
            using (var context = Bakery.Sql())
            {
                return context.Query<Image>(@"
                    SELECT
                        i.ImageId
                        ,i.ImageName
                        ,i.ImagePath
                    FROM
                        Images as i
                    JOIN
                        ProductsImages as pi
                            ON pi.ImageId = i.ImageId
                            AND pi.ProductId = @productid
                        ", new
                {
                    productid
                }).ToList();
            }
        }

        public bool InsertProductImageReference(IProductImage productImage)
        {
            if (!IsExists(productImage))
            {
                productImage.ProductImageId = GetIdForNextProductImage();

                if (productImage.ProductImageId == 0)
                {
                    productImage.ProductImageId++;
                }

                using (var context = Bakery.Sql())
                {
                    return context.Execute(@"
                    INSERT
                        ProductsImages (ProductImageId, ProductId, ImageId)
                    VALUES
                        (@productimageid, @productid, @imageid)
                    ", new
                    {
                        productimageid = productImage.ProductImageId,
                        productid = productImage.ProductId,
                        imageid = productImage.ImageId
                    }) != 0;
                }
            }
            else
            {
                return false;
            }
        }

        public int GetCountRows()
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                    SELECT COUNT(ProductImageId)       
                    FROM 
                        ProductsImages");
            }
        }

        private int GetIdForNextProductImage()
        {
            var productImageID = GetCountRows();

            while (IsExists(productImageID))
            {
                productImageID++;
            }
            return productImageID;
        }

        public bool IsExists(int productimageid)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                SELECT COUNT(ProductImageId)
                FROM
                    ProductsImages
                WHERE
                    ProductImageId = @productimageid
                ", new
                {
                    productimageid
                }) != 0;
            }
        }

        public bool IsExists(IProductImage productImage)
        {
            using (var context = Bakery.Sql())
            {
                return context.ExecuteScalar<int>(@"
                SELECT COUNT(ProductImageId)
                FROM
                    ProductsImages
                WHERE
                    ProductId  = @productid
                AND
                    ImageId = @imageid
                ", new
                {
                    productid = productImage.ProductId,
                    imageid = productImage.ImageId
                }) != 0;
            }
        }
    }
}