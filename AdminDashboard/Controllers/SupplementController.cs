using AdminDashboard.Core.Helpers;
using Techbart.DB;
using Techbart.DB.Interfaces;
using System.Web.Mvc;

namespace AdminDashboard.Controllers
{
	public class SupplementController : Controller
    {
        private readonly ISupplementRepository _supplementRepository;

        public SupplementController(ISupplementRepository supplementRepository)
        {
            _supplementRepository = supplementRepository;
        }

        // GET: Supplement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PagesData(SearchSupplementModel searchSupplement)
        {
            var supplementCount = 0;
            if (searchSupplement.Validate())
            {
                supplementCount = _supplementRepository.GetCountRows(searchSupplement);
            }
            else
            {
                supplementCount = _supplementRepository.GetCountRows();
            }

            if (supplementCount == 0)
            {
                return PartialView("SupplementsData", null);
            }

            var valideteRowsPage = new ValidateRowsPage(searchSupplement, supplementCount);

            ViewBag.PagesCount = valideteRowsPage.ValidateGetPageCount();

            var from = (searchSupplement.Page - 1) * searchSupplement.Rows;

            var to = searchSupplement.Page * searchSupplement.Rows;

            ViewBag.SearchSupplementModel = searchSupplement;

            //Get limit supplements from database
            var supplements = _supplementRepository.GetSupplements(from, to, searchSupplement);

            return PartialView("SupplementsData", supplements);
        }

        // GET: Supplement/Details/5
        public ActionResult Details(int id)
        {
            var supplement = _supplementRepository.GetSupplement(id);

            if (supplement == null)
            {
                return HttpNotFound($"Supplement with { id} ID was not fount!");
            }
            return View(supplement);
        }

        // GET: Supplement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplement/Create
        [HttpPost]
        public ActionResult Create(Supplement supplement)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _supplementRepository.InsertSupplement(supplement);

                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("","Can not add suplement");
                    }
                }
                catch
                {
                    return View();
                }
            }
            return View(supplement);
        }

        // GET: Supplement/Edit/5
        public ActionResult Edit(int id)
        {
            var supplement = _supplementRepository.GetSupplement(id);

            if (supplement == null)
            {
                return HttpNotFound($"Supplement with { id} ID was not fount!");
            }
            return View(supplement);
        }

        // POST: Supplement/Edit/5
        [HttpPost]
        public ActionResult Edit(Supplement supplement)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _supplementRepository.UpdateSupplement(supplement);

                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("","Can not update supplement!");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Server Error!");
                }
            }
            return View(supplement);
        }

        // GET: Supplement/Delete/5
        public ActionResult Delete(int id)
        {
            var supplement = _supplementRepository.GetSupplement(id);

            if (supplement == null)
            {
                return HttpNotFound($"Supplement with {supplement.SupplementId} ID not found");
            }

            return View();
        }

        // POST: Supplement/Delete/5
        [HttpPost]
        public ActionResult DeleteSupplement(int id)
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
