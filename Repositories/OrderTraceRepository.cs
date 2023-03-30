using Microsoft.EntityFrameworkCore;

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

        public OrderTraceModel GetOrderTrace(int orderNumber) 
        {
            OrderTraceModel orderTrace =  _context.OrderTrace.FirstOrDefault(ot => ot.OrderNumber == orderNumber);
            if (orderTrace == null)
            {
                return null;
            }
            return orderTrace;
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

        public void UpdateTrace(int orderNumber, string mId) 
        {
            OrderTraceModel orderTrace = GetOrderTrace(orderNumber);
            orderTrace.MachineId = mId;
            _context.OrderTrace.Update(orderTrace);
            _context.SaveChanges();
        }        
    }
}
