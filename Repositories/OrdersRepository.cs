using Microsoft.EntityFrameworkCore;
using OrderManagementMVC.Helpers;
using OrderManagementMVC.Models;
using OrderManagementMVC.ViewModels;

namespace OrderManagementMVC.Repositories
{
    public class OrdersRepository
    {
        private readonly OrderManagementContext _context;

        public OrdersRepository(OrderManagementContext context)
        {
            _context = context;
        }

        public DbSet<OrdersModel> GetAllOrders() 
        {
            return _context.Orders;
        }

        public OrdersModel GetOrdersById(int id) 
        {
            return _context.Orders.FirstOrDefault(x => x.OrderNumber == id);
        }

        public void AddOrder(OrdersModel order)
        {
            order.OrderNumber = GetOrderId();
            _context.Orders.Add(order);
            _context.SaveChanges();
            var labels = DataHelpers.CreateLabels(order);
            _context.OrderLabels.AddRange(labels);
            _context.SaveChanges();
            var traces = DataHelpers.CreateTraces(labels);
            _context.OrderTrace.AddRange(traces);
            _context.SaveChanges();
        }

        public void UpdateOrder(int id) 
        {
            var order = GetOrdersById(id);
            if (order != null && order.OrderStatus=="New")
            {
                _context.Orders.Update(order);

                var oldLabels = _context.OrderLabels.Where(l => l.OrderNumber == id);
                _context.OrderLabels.RemoveRange(oldLabels);
                var labels = DataHelpers.CreateLabels(order);
                _context.OrderLabels.AddRange(labels);
                
                var oldTraces = _context.OrderTrace.Where(ot => ot.OrderNumber == id);
                _context.OrderTrace.RemoveRange(oldTraces);
                var traces = DataHelpers.CreateTraces(labels);
                _context.OrderTrace.AddRange(traces);
                _context.SaveChanges();
            }
        }

        public void DeleteOrder(int id)
        {
            var order = GetOrdersById(id);
            if ( order != null )
            {

                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }        

        private int GetOrderId()
        {
            var orders = GetAllOrders();
            return orders.Any() ? orders.Max(x => x.OrderNumber) + 1 : 1;
        }

        public OrderTraceView GetOrderViewTraces(int orderNumber)
        {
            var order = GetOrdersById(orderNumber);
            var orderTraceView = new OrderTraceView
            {
                OrderNumber = orderNumber,
                ClientName = order.Client,
                Quantity = order.Quantity,
                orderTraceModels = _context.OrderTrace.Where(ot => ot.OrderNumber == orderNumber).ToList()
            };
            return orderTraceView;
        }

        public void UpdateOrderInternal(int orderNumber)
        {
            var order = GetOrdersById(orderNumber);
            if (order.OrderStatus == "Finished")
                return;
            var orderTraces = _context.OrderTrace.Where(ot => ot.OrderNumber == orderNumber).Where(ot => ot.MachineId != null);
            if (!orderTraces.Any())
                return;
            if (order.OrderStatus == "New")            
            {
                order.OrderStatus = "Production";
                order.DateInProduction = DateTime.Now;
            }
            
            var labels = _context.OrderLabels.Where(l => orderTraces.Any(ot => ot.IdBoxNumber == l.IdBoxNumber)).ToList();
            int total = 0;
            foreach (var trace in orderTraces)
            {
                if (trace.MachineId != null)
                {
                    total = total + labels.FirstOrDefault(l => l.IdBoxNumber == trace.IdBoxNumber).Quantity;
                }
            }
            order.Completed = total;
            if (order.Completed == order.Quantity)
            {
                order.OrderStatus = "Finished";
                order.DateFinished = DateTime.Now;
            }
            _context.Orders.Update(order);
            _context.SaveChanges();
        }
    }
}
