using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Techbart.DB;
using Techbart.DB.Interfaces;

namespace AdminDashboard.Controllers
{
	// [Attributes.Authorize]
	public class CustomerController : Controller
    {
        public readonly ICustomerRepository _customerRepository;

        public readonly IRoleTypeRepository _roleTypeRepository;

        public CustomerController(ICustomerRepository customerRepository, IRoleTypeRepository roleTypeRepository)
        {
            _customerRepository = customerRepository;
            _roleTypeRepository = roleTypeRepository;
        }

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_customerRepository.Dispose();
				_roleTypeRepository.Dispose();
			}

			base.Dispose(disposing);
		}

		#region Index
		// GET: Customer
		public ActionResult Index()
        {
			return View(new SearchCustomerModel());
        }

        public ActionResult PagesData(SearchCustomerModel searchCustomer)
        {
            return PartialView("CustomersData", _customerRepository.GetCustomers(searchCustomer));
        }

		public ActionResult ShowPager(SearchCustomerModel searchCustomer)
		{
			searchCustomer.Count = _customerRepository.Count(searchCustomer);

			return PartialView("_Pager", searchCustomer);
		}

        #endregion

        #region Details
        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            if (_customerRepository.IsExists(id))
            {
                var customer = _customerRepository.GetCustomer(id);

                return View(customer);
            }
            else
            {
                return HttpNotFound($"Can not find customer with {id} ID");
            }
        }
        #endregion

        #region Create
        // GET: Customer/Create
        public ActionResult Create()
        {
            var customer = new Customer();

            ViewBag.Roles = GetSelectListItem(customer);

            return View(customer);
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _customerRepository.InsertCustomer(customer);

                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Customer with {customer.Email} Email already exists");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", $"Server Error");
                }
            }

            ViewBag.Roles = GetSelectListItem(customer);

            return View(customer);
        }
        #endregion

        #region Edit
        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            if (_customerRepository.IsExists(id))
            {
                var customer = _customerRepository.GetCustomer(id);

                ViewBag.Roles = GetSelectListItem(customer);

                return View(customer);
            }
            return HttpNotFound($"Can not find customer with {id} ID");
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CustomerEditModel customer)
        {
            if (ModelState.IsValid)
            {
                if (_customerRepository.IsExists(id))
                {
                    customer.CustomerId = id;
                    try
                    {
                        var result = _customerRepository.UpdateCustomer(customer);

                        if (result)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", $"Error update customer with {id} ID");
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
                else
                {
                    return HttpNotFound($"Can not find customer with {id} ID");
                }
            }

            ViewBag.Roles = GetSelectListItem(customer);
            return View((Customer)customer);
        }
        #endregion

        private List<SelectListItem> GetSelectListItem(ICustomer customer)
        {
            var roleIndex = 0;
            var roles = new List<SelectListItem>();
            foreach (var roleName in _roleTypeRepository.GetRolesDescriptions())
            {
                var selected = false;
                if (customer.CustomerRole == (RoleType)roleIndex)
                {
                    selected = true;
                }
                roles.Add(new SelectListItem
                {
                    Text = roleName,
                    Value = roleIndex.ToString(),
                    Selected = selected
                });
                roleIndex++;
            }
            return roles;
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            var customer = _customerRepository.GetCustomer(id);

            if(customer == null)
            {
                return HttpNotFound($"Can not find customer with {id} ID");
            }
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerDelete(int id)
        {
            if (_customerRepository.IsExists(id))
            {
                try
                {
                    var result = _customerRepository.DeleteCustomer(id);

                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Can not delete customer with {id}");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Server error");
                }
            }
            else
            {
                return HttpNotFound($"Can not find customer with {id} ID");
            }
            return View("Delete", id);
        }
    }
}
