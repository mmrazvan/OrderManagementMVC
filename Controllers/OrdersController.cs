﻿using Microsoft.AspNetCore.Mvc;
using OrderManagementMVC.Models;
using OrderManagementMVC.Repositories;
using OrderManagementMVC.ViewModels;

namespace OrderManagementMVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrdersRepository _repo;
        private readonly LabelsRepository _labelsRepo;
        private readonly OrderLabelsRepository _orderLabelsRepo;
        private readonly OrderTraceRepository _orderTraceRepo;
        public OrdersController(OrdersRepository repo, LabelsRepository labelsRepo, OrderLabelsRepository orderLabelsRepository, OrderTraceRepository orderTraceRepo)
        {
            _repo = repo;
            _labelsRepo = labelsRepo;
            _orderLabelsRepo = orderLabelsRepository;
            _orderTraceRepo = orderTraceRepo;
        }
        public IActionResult Index()
        {
            var orders = _repo.GetAllOrders();
            return View("Index", orders);
        }

        public IActionResult Create()
        {
            var labels = _labelsRepo.GetAllLabels();
            ViewBag.Labels = labels;
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(IFormCollection collection)
        {
            OrdersModel order = new OrdersModel();
            TryUpdateModelAsync(order);
            _repo.AddOrder(order);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var order = _repo.GetOrdersById(id);
            return View("Details", order);
        }

        public IActionResult Edit(int id)
        {
            OrdersModel order = _repo.GetOrdersById(id);
            return View("Edit", order);
        }

        [HttpPost]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            OrdersModel order = _repo.GetOrdersById(id);
            TryUpdateModelAsync(order);
            _repo.UpdateOrder(id);
            return RedirectToAction("Index"); 
        }

        public IActionResult Delete(int id)
        {
            var order = _repo.GetOrdersById(id);
            return View("Delete", order);
        }

        [HttpPost]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            _orderLabelsRepo.DeleteAllOrderLabels(id);
            _orderTraceRepo.DeleteOrderTraces(id);
            _repo.DeleteOrder(id);
            return RedirectToAction("Index");
        }

        public IActionResult ViewTraces(int orderNumber)
        {
            OrderTraceView orderTraceView = _repo.GetOrderViewTraces(orderNumber);
            return View("ViewTraces", orderTraceView);
        }
    }
}
