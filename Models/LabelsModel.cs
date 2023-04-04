using System.ComponentModel.DataAnnotations;

namespace OrderManagementMVC.Models
{
    public partial class LabelsModel
    {
        public int LabelId { get; set; }
        [Required]
        [StringLength(10, MinimumLength =1)]
        public string LabelName { get; set; }
        [Required]
        [Range(25, 150)]
        public float Heigth { get; set; }
        [Required]
        [Range(50, 150)]
        public float Width { get; set; }
    }
}