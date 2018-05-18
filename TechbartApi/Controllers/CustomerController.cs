using Techbart.DB;
using Techbart.DB.Interfaces;
using Techbart.DB.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TechbartApi.Controllers
{
	public class CustomerController : ApiController
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/Customer/5
        public HttpResponseMessage Post(int id, [FromBody]LoginModel loginModel)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(loginModel))
            {
                if (id >= 0)
                {
                    var customer = _customerRepository.GetCustomer(id);

                    return customer != null ?
                        Request.CreateResponse(HttpStatusCode.OK, customer)
                        : Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Can not find the customer with {id} ID");
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
                var customers = _customerRepository.GetCustomers();

                return customers.Any() ? Request.CreateResponse(HttpStatusCode.OK, customers)
                                        : Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Can not found customers");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Invalid credentials");
            }
        }

        public HttpResponseMessage Put([FromBody]CustomerLoginRequest customerLogin)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(customerLogin))
            {
                var result = _customerRepository.InsertCustomer(customerLogin);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The customer was added");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The customer was not added");
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

                var result = _customerRepository.DeleteCustomer(id);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"The customer with {id} ID was deleted!");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"The customer with {id} ID was not deleted");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid credentials!");
            }
        }
    }
}
