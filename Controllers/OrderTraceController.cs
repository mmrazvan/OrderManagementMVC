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

                             
        public ActionResult Edit(int id)
        {
            var orderTrace = _repository.GetOrderTraceById(id);
            return View("Edit", orderTrace);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {            
            OrderTraceModel orderTrace = _repository.GetOrderTraceById(id);
            TryUpdateModelAsync(orderTrace);
            orderTrace.DateOut = DateTime.Now;
            _repository.UpdateTrace(orderTrace);
            _ordersRepository.UpdateOrderInternal(orderTrace.OrderNumber);
            return RedirectToAction(nameof(Index));
        }        
    }
}
