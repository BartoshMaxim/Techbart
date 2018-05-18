using Techbart.DB;
using Techbart.DB.Interfaces;
using Techbart.DB.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TechbartApi.Controllers
{
	public class ImageController : ApiController
    {
        private readonly IImageRepository _imageRepository;

        private readonly ICustomerRepository _customerRepository;

        public ImageController(IImageRepository imageRepository, ICustomerRepository customerRepository)
        {
            _imageRepository = imageRepository;
            _customerRepository = customerRepository;
        }

        public HttpResponseMessage Get()
        {
            var images = _imageRepository.GetImages();
            return images.Any() ?
                Request.CreateResponse(HttpStatusCode.OK, images)
                : Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Can not find any images!");
        }

        public HttpResponseMessage Get(int id)
        {
            if (id >= 0)
            {
                var image = _imageRepository.GetImage(id);
                return image != null ?
                    Request.CreateResponse(HttpStatusCode.OK, image)
                    : Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Can not find image with {id} ID!");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The identifier must be the same or greater than 0!");
            }
        }

        // POST: api/Image
        public HttpResponseMessage Post()
        {
            var images = _imageRepository.GetImages();
            return images.Any() ?
                Request.CreateResponse(HttpStatusCode.OK, images)
                : Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Can not find any images!");
        }

        public HttpResponseMessage Post(int id)
        {
            if (id >= 0)
            {
                var image = _imageRepository.GetImage(id);
                return image != null ?
                    Request.CreateResponse(HttpStatusCode.OK, image)
                    : Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Can not find image with {id} ID!");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The identifier must be the same or greater than 0!");
            }
        }

        // PUT: api/Image/5
        public HttpResponseMessage Put([FromBody]ImageLoginRequest imageLogin)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(imageLogin))
            {
                var result = _imageRepository.InsertImage(imageLogin);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The image was added");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The image was not added");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid credentials!");
            }
        }

        // DELETE: api/Image/5
        public HttpResponseMessage Delete(int id, [FromBody]LoginModel loginModel)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(loginModel))
            {

                var result = _imageRepository.DeleteImage(id);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The image with {id} ID was deleted!");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The image with {id} ID was not deleted");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid credentials!");
            }
        }
    }
}
