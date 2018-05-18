using Techbart.DB;
using Techbart.DB.Interfaces;
using Techbart.DB.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TechbartApi.Controllers
{
	public class ProductController : ApiController
    {
        private readonly IProductRepository _productRepository;

        private readonly ICustomerRepository _customerRepository;

        public ProductController(IProductRepository productRepository, ICustomerRepository customerRepository)
        {
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }

        // GET: api/product
        public HttpResponseMessage Get()
        {
            var products = _productRepository.GetProducts();
            return products.Any()?
                Request.CreateResponse(HttpStatusCode.OK, products)
                : Request.CreateResponse(HttpStatusCode.InternalServerError, "Can not find any products!");
        }

        // GET: api/product/5
        public HttpResponseMessage Get(int id)
        {
            if (id >= 0)
            {
                var product = _productRepository.GetProduct(id);
                return product != null ?
                    Request.CreateResponse(HttpStatusCode.OK, product)
                    : Request.CreateResponse(HttpStatusCode.BadRequest, $"Can not find product with {id} ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "The identifier must be the same or greater than 0!");
            }
        }

        // POST: api/product
        public HttpResponseMessage Post()
        {
            var products = _productRepository.GetProducts();
            return products.Any()?
                Request.CreateResponse(HttpStatusCode.OK, products)
                : Request.CreateResponse(HttpStatusCode.InternalServerError, "Can not find any products!");
        }

        public HttpResponseMessage Post(int id)
        {
            if (id >= 0)
            {
                var product = _productRepository.GetProduct(id);
                return product != null ?
                    Request.CreateResponse(HttpStatusCode.OK, product)
                    : Request.CreateResponse(HttpStatusCode.BadRequest, $"Can not find product with {id} ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "The identifier must be the same or greater than 0!");
            }
        }


        public HttpResponseMessage Put([FromBody]ProductLoginRequest productLogin)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(productLogin))
            {
                var result = _productRepository.InsertProduct(productLogin);
                if (result!=0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The product was added");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The product was not added");
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

                var result = _productRepository.DeleteProduct(id);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The product with {id} ID was deleted!");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The product with {id} ID was not deleted");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid credentials!");
            }
        }
    }
}
