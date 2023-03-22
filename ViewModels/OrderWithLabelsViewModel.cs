using System.ComponentModel.DataAnnotations;

using OrderManagementMVC.Models;

namespace OrderManagementMVC.ViewModels
{
    public class OrderWithLabelsViewModel
    {
        [Key]
        public int OrderNumber { get; set; }
        public string DocumentFormat { get; set; }
        public string EnvelopeType { get; set; }
        public int Quantity { get; set; }
        public int PagesOnEnvelope { get; set; }
        public string LabelType { get; set; }

        public List<OrderLabelsModel> OrderLabels { get; set; } = new List<OrderLabelsModel>();
    }
}
