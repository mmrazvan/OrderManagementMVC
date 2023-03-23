using Microsoft.EntityFrameworkCore;
using OrderManagementMVC.Helpers;
using OrderManagementMVC.Models;

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
            var labels = DataHelpers.CreateLabels(order);
            _context.OrderLabels.AddRange(labels);
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

        private int GetOrderId()
        {
            var orders = GetAllOrders();
            return orders.Any() ? orders.Max(x => x.OrderNumber) + 1 : 1;
        }

    }
}
