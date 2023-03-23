using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderManagementMVC.Controllers
{
    public class OrderLabelsController : Controller
    {
        // GET: OrderLabelsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OrderLabelsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderLabelsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderLabelsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderLabelsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderLabelsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderLabelsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderLabelsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
