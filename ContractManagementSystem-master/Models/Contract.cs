using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagementSystem.Models
{
    public enum ContractStatus
    {
        [Display(Name = "Draft")]
        Draft,

        [Display(Name = "Active")]
        Active,

        [Display(Name = "Expired")]
        Expired,

        [Display(Name = "On Hold")]
        OnHold
    }

    public enum ServiceLevel
    {
        [Display(Name = "Basic - Standard Support")]
        Basic,

        [Display(Name = "Standard - Priority Support")]
        Standard,

        [Display(Name = "Premium - 24/7 Support")]
        Premium,

        [Display(Name = "Enterprise - Dedicated Support")]
        Enterprise
    }

    public class Contract
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Client is required")]
        [Display(Name = "Client")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Contract Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Contract End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Contract Status")]
        public ContractStatus Status { get; set; } = ContractStatus.Draft;

        [Required]
        [Display(Name = "Service Level")]
        public ServiceLevel ServiceLevel { get; set; }

        [Display(Name = "Signed Agreement")]
        public string? SignedAgreementPath { get; set; }

        [Display(Name = "Agreement File Name")]
        public string? SignedAgreementFileName { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }

        public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();

        [Display(Name = "Contract Period")]
        public string ContractPeriod => $"{StartDate:yyyy-MM-dd} to {EndDate:yyyy-MM-dd}";

        [Display(Name = "Duration (Days)")]
        public int DurationDays => (EndDate - StartDate).Days;

        [Display(Name = "Is Active")]
        public bool IsActive => Status == ContractStatus.Active &&
                                 StartDate <= DateTime.UtcNow &&
                                 EndDate >= DateTime.UtcNow;

        [Display(Name = "Remaining Days")]
        public int RemainingDays => IsActive ? (EndDate - DateTime.UtcNow).Days : 0;

        [Display(Name = "Service Request Count")]
        public int ServiceRequestCount => ServiceRequests?.Count ?? 0;

        [Display(Name = "Total Service Cost")]
        [DataType(DataType.Currency)]
        public decimal TotalServiceCost => ServiceRequests?.Sum(sr => sr.Cost) ?? 0;
    }
}