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

        public void AddOrderTraces(List<OrderTraceModel> traces)
        {
            _context.OrderTrace.AddRange(traces);
            _context.SaveChanges();
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
        
    }
}
