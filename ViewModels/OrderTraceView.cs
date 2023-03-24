using System.ComponentModel.DataAnnotations;

using OrderManagementMVC.Models;

namespace OrderManagementMVC.ViewModels
{
    public class OrderTraceView
    {
        [Key]
        public int OrderNumber { get; set; }
        public string ClientName { get; set; }
        public int Quantity { get; set; }

        public List<OrderTraceModel> orderTraceModels { get; set; }
    }
}
