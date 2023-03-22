namespace OrderManagementMVC.Models
{
    public partial class OrdersModel
    {
        public int OrderNumber { get; set; }
        public string Client { get; set; }
        public string DocumentName { get; set; }
        public string DocumentFormat { get; set; }
        public string EnvelopeType { get; set; }
        public int Quantity { get; set; }
        public int PagesOnEnvelope { get; set; }
        public string LabelType { get; set; }
        public string OrderStatus { get; set; } = "New";
        public int Completed { get; set; } = 0;
        public DateTime DateInSystem { get; set; } = DateTime.Now;
        public DateTime? DateFinished { get; set; } = null;
        public DateTime? DateInProduction { get; set; } = null;
        public bool HasCustomSort { get; set; }
        public string? CustomSortFile { get; set; } = null;
        public string? CustomSortField { get; set; } = null;

        public virtual ICollection<OrderLabelsModel> OrderLabels { get; set; }
    }
}