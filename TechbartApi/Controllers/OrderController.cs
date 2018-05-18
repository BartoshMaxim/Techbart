using Techbart.DB;
using Techbart.DB.Interfaces;
using Techbart.DB.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TechbartApi.Controllers
{
	public class OrderController : ApiController
    {
        private readonly IOrderRepository _orderRepository;

        private readonly ICustomerRepository _customerRepository;

        public OrderController(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public HttpResponseMessage Post(int id, [FromBody]LoginModel loginModel)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(loginModel))
            {
                if (id >= 0)
                {
                    var order = _orderRepository.GetOrder(id);

                    return order != null ?
                        Request.CreateResponse(HttpStatusCode.OK, order)
                        : Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Can not find the order with {id} ID");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The identifier must be the same or greater than 0!");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Invalid credentials");
            }
        }

        public HttpResponseMessage Post([FromBody]LoginModel loginModel)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(loginModel))
            {
                var orders = _orderRepository.GetOrders();

                return orders.Any() ? Request.CreateResponse(HttpStatusCode.OK, orders)
                                        : Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Can not found customers");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Invalid credentials");
            }
        }

        public HttpResponseMessage Put([FromBody]OrderLoginRequest orderLogin)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(orderLogin))
            {
                var result = _orderRepository.InsertOrder(orderLogin);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The order was added");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The order was not added");
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
                var result = _orderRepository.DeleteOrder(id);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The order with {id} ID was deleted!");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The order with {id} ID was not deleted");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid credentials!");
            }
        }
    }
}
