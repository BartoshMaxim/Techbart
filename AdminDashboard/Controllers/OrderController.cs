using AdminDashboard.Core.Helpers;
using Techbart.DB;
using Techbart.DB.Interfaces;
using System.Web.Mvc;

namespace AdminDashboard.Controllers
{
	public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PagesData(SearchOrderModel searchOrder)
        {
            var customersCount = 0;
            if (searchOrder.Validate())
            {
                customersCount = _orderRepository.GetCountRows(searchOrder);
            }
            else
            {
                customersCount = _orderRepository.GetCountRows();
            }

            if (customersCount == 0)
            {
                return PartialView("_OrdersData", null);
            }

            var valideteRowsPage = new ValidateRowsPage(searchOrder, customersCount);

            ViewBag.PagesCount = valideteRowsPage.ValidateGetPageCount();

            var from = (searchOrder.Page - 1) * searchOrder.Rows;

            var to = searchOrder.Page * searchOrder.Rows;

            ViewBag.SearchOrderModel = searchOrder;

            //Get limit orders from database
            var customers = _orderRepository.GetOrders(from, to, searchOrder);

            return PartialView("_OrdersData", customers);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
