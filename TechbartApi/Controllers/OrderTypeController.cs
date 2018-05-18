using Techbart.DB;
using Techbart.DB.Interfaces;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TechbartApi.Controllers
{
	public class OrderTypeController : ApiController
    {
        private readonly IOrderTypeRepository _orderTypeRepository;

        private readonly ICustomerRepository _customerRepository;

        public OrderTypeController(IOrderTypeRepository orderTypeRepository, ICustomerRepository customerRepository)
        {
            _orderTypeRepository = orderTypeRepository;
            _customerRepository = customerRepository;
        }

        public HttpResponseMessage Get()
        {
            var orderTypes = _orderTypeRepository.GetOrderTypes();
            return orderTypes.Any() ?
                Request.CreateResponse(HttpStatusCode.OK, orderTypes)
                : Request.CreateResponse(HttpStatusCode.InternalServerError, "Can not find any ordertype!");
        }

        public HttpResponseMessage Post()
        {
            var orderTypes = _orderTypeRepository.GetOrderTypes();
            return orderTypes.Any() ?
                Request.CreateResponse(HttpStatusCode.OK, orderTypes)
                : Request.CreateResponse(HttpStatusCode.InternalServerError, "Can not find any ordertype!");
        }

        public HttpResponseMessage Post(int id, [FromBody]LoginModel loginModel)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(loginModel))
            {
                if (id >= 0)
                {
                    var orders = _orderTypeRepository.GetOrders(id);
                    return orders.Any() ?
                        Request.CreateResponse(HttpStatusCode.OK, orders)
                        : Request.CreateResponse(HttpStatusCode.BadRequest, $"Can not find orders of the ordertype with {id} ID!");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "The identifier must be the same or greater than 0!");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid credentials!");
            }
        }
    }
}
