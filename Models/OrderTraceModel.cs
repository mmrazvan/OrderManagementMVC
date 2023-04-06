#nullable disable

namespace OrderManagementMVC.Models
{
    public partial class OrderTraceModel
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string IdBoxNumber { get; set; }
        public DateTime? DateOut { get; set; }
        public string MachineId { get; set; }
    }
}