using System.Web.Mvc;
using Techbart.DB;
using Techbart.DB.Interfaces;

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
            return View(new SearchSupplementModel());
        }

        public ActionResult PagesData(SearchSupplementModel searchSupplement)
        {
            return PartialView("SupplementsData", _supplementRepository.GetSupplements(searchSupplement));
        }

		public ActionResult ShowPager(SearchSupplementModel searchSupplement)
		{
			searchSupplement.Count = _supplementRepository.Count(searchSupplement);

			return PartialView("_Pager", searchSupplement);
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
