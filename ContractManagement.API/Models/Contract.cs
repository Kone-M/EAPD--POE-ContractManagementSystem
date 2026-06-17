using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagement.API.Models;

public enum ContractStatus
{
    Draft = 0,
    Active = 1,
    Expired = 2,
    OnHold = 3
}

public enum ServiceLevel
{
    Basic = 0,
    Standard = 1,
    Premium = 2,
    Enterprise = 3
}

public class Contract
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ClientId { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    public ContractStatus Status { get; set; } = ContractStatus.Draft;

    public ServiceLevel ServiceLevel { get; set; }

    public string? SignedAgreementPath { get; set; }

    public string? SignedAgreementFileName { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("ClientId")]
    public virtual Client? Client { get; set; }

    public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();

    // Computed Properties
    [NotMapped]
    public bool IsActive => Status == ContractStatus.Active &&
                            StartDate <= DateTime.UtcNow &&
                            EndDate >= DateTime.UtcNow;

    [NotMapped]
    public int DurationDays => (EndDate - StartDate).Days;

    [NotMapped]
    public int ServiceRequestCount => ServiceRequests?.Count ?? 0;

    [NotMapped]
    public decimal TotalServiceCost => ServiceRequests?.Sum(sr => sr.Cost) ?? 0;

    [NotMapped]
    public int RemainingDays => IsActive ? (EndDate - DateTime.UtcNow).Days : 0;
}