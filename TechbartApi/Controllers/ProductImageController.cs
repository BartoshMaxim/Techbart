using Techbart.DB;
using Techbart.DB.Interfaces;
using Techbart.DB.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;

namespace TechbartApi.Controllers
{
    public class ProductImageController : ApiController
    {
        private readonly IProductImageRepository _productImageRepository;

        private readonly ICustomerRepository _customerRepository;

        public ProductImageController(IProductImageRepository productImageRepository, ICustomerRepository customerRepository)
        {
            _productImageRepository = productImageRepository;
            _customerRepository = customerRepository;
        }

        public HttpResponseMessage Get(int id)
        {
            if (id >= 0)
            {
                var products = _productImageRepository.GetImages(id);
                return products.Any() ?
                    Request.CreateResponse(HttpStatusCode.OK, products)
                    : Request.CreateResponse(HttpStatusCode.BadRequest, $"Can not find images of the product with {id} ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "The identifier must be the same or greater than 0!");
            }
        }

        public HttpResponseMessage Post(int id)
        {
            if (id >= 0)
            {
                var products = _productImageRepository.GetImages(id);
                return products.Any() ?
                    Request.CreateResponse(HttpStatusCode.OK, products)
                    : Request.CreateResponse(HttpStatusCode.BadRequest, $"Can not find images of the product with {id} ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "The identifier must be the same or greater than 0!");
            }
        }


        public HttpResponseMessage Put([FromBody]ProductImageLoginRequest productImageLogin)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(productImageLogin))
            {
                var result = _productImageRepository.InsertProductImageReference(productImageLogin);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The productimage was added");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The productimage was not added");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid credentials!");
            }
        }

        public HttpResponseMessage Delete(int id, [FromBody]LoginModel loginModel)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(loginModel))
            {

                var result = _productImageRepository.DeleteProductImageReference(id);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The productimage with {id} ID was deleted!");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The productimage with {id} ID was not deleted");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid credentials!");
            }
        }

        public HttpResponseMessage Delete([FromBody]ProductImageLoginRequest productImageLogin)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(productImageLogin))
            {
                var productImageId = _productImageRepository.GetProductImageId(productImageLogin);

                var result = _productImageRepository.DeleteProductImageReference(productImageLogin);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The productimage with {productImageId} ID was deleted!");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The productimage with {productImageLogin.ProductImageId} ID was not deleted");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid credentials!");
            }
        }
    }
}
