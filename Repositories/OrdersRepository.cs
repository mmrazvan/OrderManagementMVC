using Microsoft.EntityFrameworkCore;

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
        }

        public void UpdateOrder(int id) 
        {
            var order = GetOrdersById(id);
            if (order != null)
            {
                _context.Orders.Update(order);
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

        public OrderWithLabelsViewModel GetOrderWithLabels(int orderNumber)
        {
            OrderWithLabelsViewModel orderWithLabels = new OrderWithLabelsViewModel();
            
            OrdersModel order = GetOrdersById(orderNumber);

            orderWithLabels.OrderNumber = orderNumber;
            orderWithLabels.LabelType = order.LabelType;
            orderWithLabels.Quantity = order.Quantity;
            orderWithLabels.DocumentFormat = order.DocumentFormat;
            orderWithLabels.EnvelopeType = order.EnvelopeType;
            orderWithLabels.PagesOnEnvelope = order.PagesOnEnvelope;
            IEnumerable<OrderLabelsModel> labels = _context.OrderLabels.Where(x => x.OrderNumber == orderNumber);
            if (!labels.Any())
            {
                foreach (var item in labels)
                {
                    orderWithLabels.OrderLabels.Add(item);
                } 
            }
            return orderWithLabels;
        }

        private int GetOrderId()
        {
            var orders = GetAllOrders();
            return orders.Any() ? orders.Max(x => x.OrderNumber) + 1 : 1;
        }

    }
}
