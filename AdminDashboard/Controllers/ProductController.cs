using AdminDashboard.Core.ControllersLogic;
using AdminDashboard.Core.Helpers;
using Techbart.DB;
using Techbart.DB.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AdminDashboard.Controllers
{
	public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        private readonly IImageRepository _imageRepository;

        private readonly IProductImageRepository _productImageRepository;

        public ProductController(IProductRepository productRepository, IImageRepository imageRepository, IProductImageRepository productImageRepository)
        {
            _productRepository = productRepository;
            _imageRepository = imageRepository;
            _productImageRepository = productImageRepository;
        }

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_imageRepository.Dispose();
				_productRepository.Dispose();
				_productImageRepository.Dispose();
			}

			base.Dispose(disposing);
		}

		public ActionResult Index()
        {
            return View(new SearchProductModel());
        }

        public ActionResult PagesData(SearchProductModel searchProduct)
        {
            return PartialView("ProductsData", _productRepository.GetProducts(searchProduct));
        }

		public ActionResult ShowPager(SearchProductModel searchProduct)
		{
			searchProduct.Count = _productRepository.Count(searchProduct);

			return PartialView("_Pager", searchProduct);
		}

		public ActionResult Details(int id)
        {
            var product = _productRepository.GetProduct(id);

            var images = _productImageRepository.GetImages(product.ProductId);

            var image = _imageRepository.GetImage(product.ImageId);

            ViewBag.PreviewImage = image;

            ViewBag.Images = images;

            if (product == null)
            {
                return RedirectToAction("Index", $"Product with {id} ID not found!");
            }

            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProductModel productModel)
        {
            if (!ProductHepler.ImageIsExistsInCreateProductModel(productModel))
            {
                ModelState.AddModelError("Image", "Upload any images");
            }

            if (productModel.Files.Count() > 8)
            {
                ModelState.AddModelError("Image", "Count images can not be more 8.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var imageidList = new List<int>();

                    // Upload Product Photos
                    foreach (var file in productModel.Files)
                    {
                        if (file != null)
                        {
                            var result = ImageHelper.UploadImage(new UploadImageModel { ImageFile = file, ImageName = file.FileName }, _imageRepository, Server);

                            if (result > 0)
                            {
                                imageidList.Add(result);
                            }
                        }
                    }

                    productModel.ImageId = imageidList.FirstOrDefault();

                    //Insert Product to DB
                    var productid = _productRepository.InsertProduct(productModel);

                    //Create ProductImage References
                    foreach (var imageid in imageidList)
                    {
                        _productImageRepository.InsertProductImageReference(new ProductImage
                        {
                            ProductId = productid,
                            ImageId = imageid
                        });
                    }

                    if (productid != 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Product was not created!");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Server Error!");
                }
            }

            return View();
        }


        public ActionResult Edit(int id)
        {
            var product = default(Product);

            product = (Product)_productRepository.GetProduct(id);

            if (product == null)
            {
                return HttpNotFound($"Can not find product with {id} ID");
            }

            var images = _productImageRepository.GetImages(id);

            if (images != null)
            {
                ViewBag.Images = images;
            }

            return View((EditProductModel)product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditProductModel editProduct)
        {
            if (ModelState.IsValid && _productRepository.IsExists(editProduct.ProductId))
            {
                try
                {
                    var imageidList = new List<int>();

                    // Upload Product Photos
                    foreach (var file in editProduct.Files)
                    {
                        if (file != null)
                        {
                            var result = ImageHelper.UploadImage(new UploadImageModel { ImageFile = file, ImageName = file.FileName }, _imageRepository, Server);

                            if (result > 0)
                            {
                                imageidList.Add(result);
                            }
                        }
                    }

                    if (_productImageRepository.GetImages(editProduct.ProductId).Count == 0)
                    {
                        editProduct.ImageId = imageidList.FirstOrDefault();
                    }

                    //Create ProductImage References
                    foreach (var imageid in imageidList)
                    {
                        _productImageRepository.InsertProductImageReference(new ProductImage
                        {
                            ProductId = editProduct.ProductId,
                            ImageId = imageid
                        });
                    }

					//Update Product
					var updateResult = _productRepository.UpdateProduct(editProduct);

                    if (updateResult)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Product with {editProduct.ProductId} ID was not updated!");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Server Error!");
                }
            }

            return View(editProduct);
        }

        public ActionResult Delete(int id)
        {
            var product = _productRepository.GetProduct(id);

            var images = _productImageRepository.GetImages(product.ProductId);

            ViewBag.Images = images;

            if (product == null)
            {
                return HttpNotFound($"Can not find product with {id} ID");
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int id)
        {
            if (_productRepository.IsExists(id))
            {
                try
                {
                    var result = _productRepository.DeleteProduct(id);

                    if (result)
                    {

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Can not delete product with {id}");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Server error");
                }
            }
            else
            {
                return HttpNotFound($"Can not find product with {id} ID");
            }

            return View("Delete", id);
        }

        [HttpPost]
        public void DeleteImage(ProductImage productImage)
        {
            if (_productImageRepository.IsExists(productImage))
            {
                _productImageRepository.DeleteProductImageReference(productImage);

                var product = _productRepository.GetProduct(productImage.ProductId);

                if (product.ImageId == productImage.ImageId)
                {
                    var resultProductImage = _productImageRepository.GetImages(productImage.ProductId).FirstOrDefault();
                    product.ImageId = resultProductImage != null ? resultProductImage.ImageId : 0;

                    _productRepository.UpdateProduct(product);
                }

                _imageRepository.DeleteImage(productImage.ImageId);
            }
        }
    }
}
