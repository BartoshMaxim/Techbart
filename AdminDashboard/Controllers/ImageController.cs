using AdminDashboard.Core.ControllersLogic;
using System.Web.Mvc;
using Techbart.DB;
using Techbart.DB.Interfaces;

namespace AdminDashboard.Controllers
{
	public class ImageController : Controller
    {
        private readonly IImageRepository _imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_imageRepository.Dispose();
			}

			base.Dispose(disposing);
		}

		// GET: Image
		public ActionResult Index()
        {
			return View(new SearchImageModel());
        }

        public ActionResult PagesData(SearchImageModel searchImage)
        {
			return PartialView("ImagesData", _imageRepository.GetImages(searchImage));
        }

		public ActionResult ShowPager(SearchImageModel searchImage)
		{
			searchImage.Count = _imageRepository.Count(searchImage);

			return PartialView("_Pager", searchImage);
		}

		// GET: Image/Details/5
		public ActionResult Details(int id)
        {
            var image = _imageRepository.GetImage(id);

            if (image == null)
            {
                return HttpNotFound($"Image with {id} ID was not fount!");
            }

            return View(image);
        }

        // GET: Image/Create
        public ActionResult Upload()
        {
            return View();
        }

        // POST: Image/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(UploadImageModel imageModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = ImageHelper.UploadImage(imageModel, _imageRepository, Server);

                    if (result != 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Can't add this image!");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Server Error");
                }
            }
            return View(imageModel);
        }

        // GET: Image/Edit/5
        public ActionResult Edit(int id)
        {
            var image = _imageRepository.GetImage(id);

            if (image == null)
            {
                return HttpNotFound($"Image with { id} ID was not fount!");
            }

            return View(image);
        }

        // POST: Image/Edit/5
        [HttpPost]
        public ActionResult Edit(Image image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _imageRepository.UpdateImage(image);

                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Image with {image.ImageId} ID was not update!");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Server Error!");
                }
            }
            return View(image);
        }

        // GET: Image/Delete/5
        public ActionResult Delete(int id)
        {
            var image = _imageRepository.GetImage(id);

            if (image == null)
            {
                return HttpNotFound($"Image with { id} ID was not fount!");
            }
            return View(image);
        }

        // POST: Image/Delete/5
        [HttpPost]
        public ActionResult ImageDelete(int id)
        {
            if (_imageRepository.IsExists(id))
            {
                try
                {
                    var image = _imageRepository.GetImage(id);

                    var pathToDeleteImage = Server.MapPath(image.ImagePath);

                    System.IO.File.Delete(pathToDeleteImage);

                    var result = _imageRepository.DeleteImage(id);

                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return HttpNotFound($"Image with { id} ID was not fount!");
                    }
                }
                catch
                {
                    Response.StatusCode = 500;
                    return RedirectToAction("Index", $"Server Error!");
                }
            }
            else
            {
                return HttpNotFound($"Image with { id} ID was not fount!");
            }
        }

        public ActionResult UploadImageModel()
        {
            return PartialView("_UploadImage");
        }
    }
}
