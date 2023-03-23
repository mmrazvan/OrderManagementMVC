using OrderManagementMVC.Models;

namespace OrderManagementMVC.Repositories
{
    public class OrderLabelsRepository
    {
        private readonly OrderManagementContext _context;

        public OrderLabelsRepository(OrderManagementContext context)
        {
            _context = context;
        }

        public void AddOrderLabel(OrderLabelsModel orderLabel)
        {
            _context.OrderLabels.Add(orderLabel);
            _context.SaveChanges();
        }

        public void AddOrderLabels(IEnumerable<OrderLabelsModel> orderLabels)
        {
            foreach (var label in orderLabels)
            {
                _context.OrderLabels.Add(label);
            }            
            _context.SaveChanges();
        }

        public IEnumerable<OrderLabelsModel> GetOrderLabelsById(int id)
        {
            return _context.OrderLabels.Where(label => label.OrderNumber == id).ToList();
        }

        public List<OrderLabelsModel> GetLabelsFromOrder(int orderNumber)
        {
            return _context.OrderLabels.Where(label => label.OrderNumber ==  orderNumber).ToList();
        }

        public void DeleteAllOrderLabels(int orderNumber)
        {
            _context.OrderLabels.RemoveRange(GetLabelsFromOrder(orderNumber));
            _context.SaveChanges();
        }
    }
}
