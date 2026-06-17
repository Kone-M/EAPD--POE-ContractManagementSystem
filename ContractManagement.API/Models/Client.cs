using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagement.API.Models;

public class Client
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Client name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required")]
    [StringLength(200)]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Region is required")]
    [StringLength(50)]
    public string Region { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    // Computed Properties
    [NotMapped]
    public int ActiveContractsCount => Contracts?.Count(c => c.IsActive) ?? 0;

    [NotMapped]
    public int TotalContractsCount => Contracts?.Count ?? 0;
}