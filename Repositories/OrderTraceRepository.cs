using Microsoft.EntityFrameworkCore;

using OrderManagementMVC.DataContext;
using OrderManagementMVC.Models;

namespace OrderManagementMVC.Repositories
{
    public class OrderTraceRepository
    {
        private readonly OrderManagementContext _context;

        public OrderTraceRepository(OrderManagementContext context)
        {
            _context = context;
        }

        public DbSet<OrderTraceModel> GetAllTraces()
        {
            return _context.OrderTrace;
        }

        public void AddOrderTraces(List<OrderTraceModel> traces)
        {
            _context.OrderTrace.AddRange(traces);
            _context.SaveChanges();
        }

        public OrderTraceModel GetOrderTraceById(int id)
        {
            return _context.OrderTrace.FirstOrDefault(t => t.Id == id);
        }
        public List<OrderTraceModel> GetOrderTraces(int orderNumber) 
        {
            return _context.OrderTrace.Where(ot => ot.OrderNumber == orderNumber).ToList();
        }

        public void DeleteOrderTraces(int orderNumber)
        {
            _context.OrderTrace.RemoveRange(GetOrderTraces(orderNumber));
            _context.SaveChanges();
        }

        public void UpdateTrace(OrderTraceModel orderTrace) 
        {
            _context.OrderTrace.Update(orderTrace);
            _context.SaveChanges();
        }        
    }
}
