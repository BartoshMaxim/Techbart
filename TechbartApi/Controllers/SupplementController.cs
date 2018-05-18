using Techbart.DB;
using Techbart.DB.Interfaces;
using Techbart.DB.Models.ApiControllerEntities;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TechbartApi.Controllers
{
	public class SupplementController : ApiController
    {
        private readonly ISupplementRepository _supplementRepository;

        private readonly ICustomerRepository _customerRepository;

        public SupplementController(ISupplementRepository supplementRepository, ICustomerRepository customerRepository)
        {
            _supplementRepository = supplementRepository;
            _customerRepository = customerRepository;
        }

        public HttpResponseMessage Get()
        {
            var supplements = _supplementRepository.GetSupplements();
            return supplements.Any() ?
                Request.CreateResponse(HttpStatusCode.OK, supplements)
                : Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Can not find any supplemets!");
        }

        public HttpResponseMessage Get(int id)
        {
            if (id >= 0)
            {
                var supplement = _supplementRepository.GetSupplement(id);
                return supplement != null ?
                    Request.CreateResponse(HttpStatusCode.OK, supplement)
                    : Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Can not find supplement with {id} ID!");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The identifier must be the same or greater than 0!");
            }
        }
        
        public HttpResponseMessage Post()
        {
            var supplements = _supplementRepository.GetSupplements();
            return supplements.Any() ?
                Request.CreateResponse(HttpStatusCode.OK, supplements)
                : Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Can not find any supplemets!");
        }

        public HttpResponseMessage Post(int id)
        {
            if (id >= 0)
            {
                var supplement = _supplementRepository.GetSupplement(id);
                return supplement != null ?
                    Request.CreateResponse(HttpStatusCode.OK, supplement)
                    : Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Can not find supplement with {id} ID!");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The identifier must be the same or greater than 0!");
            }
        }
        
        public HttpResponseMessage Put([FromBody]SupplementLoginRequest supplementLogin)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(supplementLogin))
            {
                var result = _supplementRepository.InsertSupplement(supplementLogin);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The supplement was added");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The supplement was not added");
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

                var result = _supplementRepository.DeleteSupplement(id);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The supplement with {id} ID was deleted!");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The supplement with {id} ID was not deleted");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid credentials!");
            }
        }
    }
}
