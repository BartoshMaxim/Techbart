using Techbart.DB;
using Techbart.DB.Interfaces;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TechbartApi.Controllers
{
	public class RoleTypeController : ApiController
    {
        private readonly IRoleTypeRepository _roleTypeRepository;

        private readonly ICustomerRepository _customerRepository;

        public RoleTypeController(IRoleTypeRepository roleTypeRepository, ICustomerRepository customerRepository)
        {
            _roleTypeRepository = roleTypeRepository;
            _customerRepository = customerRepository;
        }

        // GET: api/Customer/5
        public HttpResponseMessage Post(int id, [FromBody]LoginModel loginModel)
        {
            if (ModelState.IsValid && _customerRepository.IsAdmin(loginModel))
            {
                if (id >= 0)
                {
                    var customers = _roleTypeRepository.GetCustomers(id);

                    return customers.Any() ?
                        Request.CreateResponse(HttpStatusCode.OK, customers)
                        : Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Can not find the customer of the roletype with {id} ID");
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
                var roletypes = _roleTypeRepository.GetRoleTypes();

                return roletypes.Any() ? Request.CreateResponse(HttpStatusCode.OK, roletypes)
                                        : Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Can not found roletypes");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Invalid credentials");
            }
        }
    }
}
