using Microsoft.AspNetCore.Mvc;

using OrderManagementMVC.Models;
using OrderManagementMVC.Repositories;

namespace OrderManagementMVC.Controllers
{
    public class OrderTraceController : Controller
    {
        private readonly OrderTraceRepository _repository;
        private readonly OrdersRepository _ordersRepository;
        public OrderTraceController(OrderTraceRepository repository, OrdersRepository ordersRepository)
        {
            _repository = repository;
            _ordersRepository = ordersRepository;
        }
        // GET: OrderTraceController
        public ActionResult Index()
        {
            var traces = _repository.GetAllTraces();
            return View("Index", traces);
        }

        // GET: OrderTraceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderTraceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderTraceController/Create
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

        // GET: OrderTraceController/Edit/5
        public ActionResult Edit(int id)
        {
            var orderTrace = _repository.GetOrderTraceById(id);
            return View("Edit", orderTrace);
        }

        // POST: OrderTraceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                OrderTraceModel orderTrace = _repository.GetOrderTraceById(id);
                TryUpdateModelAsync(orderTrace);
                orderTrace.DateOut = DateTime.Now;
                _repository.UpdateTrace(orderTrace.OrderNumber, orderTrace.MachineId);
                _ordersRepository.UpdateOrderInternal(orderTrace.OrderNumber);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderTraceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderTraceController/Delete/5
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
