using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagementSystem.Models
{
    public enum RequestStatus
    {
        [Display(Name = "Open")]
        Open,

        [Display(Name = "In Progress")]
        InProgress,

        [Display(Name = "Completed")]
        Completed,

        [Display(Name = "Cancelled")]
        Cancelled
    }

    public class ServiceRequest
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Contract is required")]
        [Display(Name = "Contract")]
        public int ContractId { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters")]
        [Display(Name = "Service Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Cost is required")]
        [Range(0.01, 1000000, ErrorMessage = "Cost must be between 0.01 and 1,000,000")]
        [DataType(DataType.Currency)]
        [Display(Name = "Cost (USD)")]
        public decimal Cost { get; set; }

        [Required]
        [Display(Name = "Request Status")]
        public RequestStatus Status { get; set; } = RequestStatus.Open;

        [Display(Name = "Created Date")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Completed Date")]
        [DataType(DataType.DateTime)]
        public DateTime? CompletedAt { get; set; }

        // Navigation property
        [ForeignKey("ContractId")]
        public virtual Contract Contract { get; set; }

        [Display(Name = "Cost (USD)")]
        [DataType(DataType.Currency)]
        public decimal CostUSD => Cost;

        [Display(Name = "Cost (ZAR)")]
        [DataType(DataType.Currency)]
        public decimal CostZAR => Cost * 19.5m; // Exchange rate: 1 USD = 19.5 ZAR

        [Display(Name = "Processing Time")]
        public string ProcessingTime
        {
            get
            {
                if (CompletedAt.HasValue)
                {
                    var days = (CompletedAt.Value - CreatedAt).Days;
                    return $"{days} days";
                }
                return "In Progress";
            }
        }

        [Display(Name = "Status Badge")]
        public string StatusBadge
        {
            get
            {
                return Status switch
                {
                    RequestStatus.Open => "badge bg-warning",
                    RequestStatus.InProgress => "badge bg-info",
                    RequestStatus.Completed => "badge bg-success",
                    RequestStatus.Cancelled => "badge bg-danger",
                    _ => "badge bg-secondary"
                };
            }
        }
    }
}