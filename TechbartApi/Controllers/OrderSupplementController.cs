using Techbart.DB;
using Techbart.DB.Interfaces;
using Techbart.DB.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TechbartApi.Controllers
{
	public class OrderSupplementController : ApiController
    {
        private readonly IOrderSupplementRepository _orderSupplementRepository;

        private readonly ICustomerRepository _customerRepository;

        public OrderSupplementController(IOrderSupplementRepository orderSupplementRepository, ICustomerRepository customerRepository)
        {
            _orderSupplementRepository = orderSupplementRepository;
            _customerRepository = customerRepository;
        }

        public HttpResponseMessage Get(int id)
        {
            if (id >= 0)
            {
                var supplements = _orderSupplementRepository.GetSupplements(id);
                return supplements.Any()?
                    Request.CreateResponse(HttpStatusCode.OK, supplements)
                    : Request.CreateResponse(HttpStatusCode.BadRequest, $"Can not find supplements of the order with {id} ID!");
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
                var supplements = _orderSupplementRepository.GetSupplements(id);
                return supplements.Any() ?
                    Request.CreateResponse(HttpStatusCode.OK, supplements)
                    : Request.CreateResponse(HttpStatusCode.BadRequest, $"Can not find supplements of the order with {id} ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "The identifier must be the same or greater than 0!");
            }
        }


        public HttpResponseMessage Put([FromBody]OrderSupplementLoginRequest orderSupplementLogin)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(orderSupplementLogin))
            {
                var result = _orderSupplementRepository.InsertOrderSupplementReference(orderSupplementLogin);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The orderimage was added");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The orderimage was not added");
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

                var result = _orderSupplementRepository.DeleteOrderSupplementReference(id);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The ordersupplement with {id} ID was deleted!");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The ordersupplement with {id} ID was not deleted");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid credentials!");
            }
        }

        public HttpResponseMessage Delete([FromBody]OrderSupplementLoginRequest orderSupplementLogin)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(orderSupplementLogin))
            {
                var orderSupplementId = _orderSupplementRepository.GetOrderSupplementId(orderSupplementLogin);

                var result = _orderSupplementRepository.DeleteOrderSupplementReference(orderSupplementLogin);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The ordersupplement with {orderSupplementId} ID was deleted!");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The ordersupplement with {orderSupplementLogin.OrderSupplementId} ID was not deleted");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid credentials!");
            }
        }
    }
}
