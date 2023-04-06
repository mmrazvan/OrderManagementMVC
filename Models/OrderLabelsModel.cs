#nullable disable

namespace OrderManagementMVC.Models
{
    public partial class OrderLabelsModel
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string IdBoxNumber { get; set; }
        public int BoxNumber { get; set; }
        public int StartIndex { get; set; }
        public int StopIndex { get; set; }
        public int Quantity { get; set; }

        public virtual OrdersModel OrderNumberNavigation { get; set; }
    }
}