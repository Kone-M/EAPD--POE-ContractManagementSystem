using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagement.API.Models;

public enum RequestStatus
{
    Open = 0,
    InProgress = 1,
    Completed = 2,
    Cancelled = 3
}

public class ServiceRequest
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ContractId { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 10)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Range(0.01, 1000000)]
    [DataType(DataType.Currency)]
    public decimal Cost { get; set; }

    public RequestStatus Status { get; set; } = RequestStatus.Open;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; set; }

    // Navigation property
    [ForeignKey("ContractId")]
    public virtual Contract? Contract { get; set; }

    // Computed Properties
    [NotMapped]
    public decimal CostZAR => Cost * 19.5m;

    [NotMapped]
    public string? ProcessingTime => CompletedAt.HasValue ?
        $"{CompletedAt.Value.Subtract(CreatedAt).Days} days" :
        "In Progress";
}