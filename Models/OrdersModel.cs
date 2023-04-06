#nullable disable
using System.ComponentModel.DataAnnotations;

namespace OrderManagementMVC.Models
{
    public partial class OrdersModel
    {
        public OrdersModel()
        {
            OrderLabels = new HashSet<OrderLabelsModel>();
        }

        public int OrderNumber { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string Client { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string DocumentName { get; set; }
        [Required]
        [StringLength(5, MinimumLength = 1)]
        public string DocumentFormat { get; set; }
        [Required]
        [StringLength(5)]
        public string EnvelopeType { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        [Range(1, 6)]
        public int PagesOnEnvelope { get; set; }
        [StringLength(10, MinimumLength = 1)]
        public string LabelType { get; set; }
        [StringLength(20, MinimumLength = 1)]
        public string OrderStatus { get; set; } = "New";
        public int Completed { get; set; } = 0;
        public DateTime DateInSystem { get; set; } = DateTime.Now;
        public DateTime? DateFinished { get; set; }
        public DateTime? DateInProduction { get; set; }
        public bool? HasCustomSort { get; set; }=false;
        public string CustomSortFile { get; set; }
        public string CustomSortField { get; set; }

        public virtual ICollection<OrderLabelsModel> OrderLabels { get; set; }
    }
}