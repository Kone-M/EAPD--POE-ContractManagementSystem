using Microsoft.EntityFrameworkCore;
using ContractManagement.API.Models;  // ← Keep this

namespace ContractManagement.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    // ✅ FULLY QUALIFY these with the full namespace
    public DbSet<ContractManagement.API.Models.Client> Clients { get; set; }
    public DbSet<ContractManagement.API.Models.Contract> Contracts { get; set; }
    public DbSet<ContractManagement.API.Models.ServiceRequest> ServiceRequests { get; set; }
    public DbSet<ContractManagement.API.Models.User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ✅ FULLY QUALIFY these too
        modelBuilder.Entity<ContractManagement.API.Models.Contract>()
            .HasOne(c => c.Client)
            .WithMany(c => c.Contracts)
            .HasForeignKey(c => c.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ContractManagement.API.Models.ServiceRequest>()
            .HasOne(sr => sr.Contract)
            .WithMany(c => c.ServiceRequests)
            .HasForeignKey(sr => sr.ContractId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        modelBuilder.Entity<ContractManagement.API.Models.Contract>()
            .HasIndex(c => c.Status);
        modelBuilder.Entity<ContractManagement.API.Models.Contract>()
            .HasIndex(c => c.ClientId);
        modelBuilder.Entity<ContractManagement.API.Models.ServiceRequest>()
            .HasIndex(sr => sr.ContractId);
        modelBuilder.Entity<ContractManagement.API.Models.Client>()
            .HasIndex(c => c.Email)
            .IsUnique();

        // Decimal precision
        modelBuilder.Entity<ContractManagement.API.Models.ServiceRequest>()
            .Property(sr => sr.Cost)
            .HasPrecision(18, 2);

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // ✅ FULLY QUALIFY all seed data
        modelBuilder.Entity<ContractManagement.API.Models.User>().HasData(
            new ContractManagement.API.Models.User
            {
                Id = 1,
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            },
            new ContractManagement.API.Models.User
            {
                Id = 2,
                Username = "manager",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("manager123"),
                Role = "Manager",
                CreatedAt = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<ContractManagement.API.Models.Client>().HasData(
            new ContractManagement.API.Models.Client
            {
                Id = 1,
                Name = "ABC Corporation",
                Email = "contact@abccorp.com",
                Phone = "+1 (555) 123-4567",
                Address = "123 Business Avenue, New York, NY 10001",
                Region = "North America",
                CreatedAt = DateTime.UtcNow
            },
            new ContractManagement.API.Models.Client
            {
                Id = 2,
                Name = "XYZ Enterprises",
                Email = "info@xyzent.com",
                Phone = "+44 20 1234 5678",
                Address = "456 Commerce Street, London, UK",
                Region = "Europe",
                CreatedAt = DateTime.UtcNow
            },
            new ContractManagement.API.Models.Client
            {
                Id = 3,
                Name = "Tech Solutions Ltd",
                Email = "sales@techsolutions.com",
                Phone = "+61 2 9876 5432",
                Address = "789 Innovation Drive, Sydney, Australia",
                Region = "Asia Pacific",
                CreatedAt = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<ContractManagement.API.Models.Contract>().HasData(
            new ContractManagement.API.Models.Contract
            {
                Id = 1,
                ClientId = 1,
                StartDate = new DateTime(2024, 1, 1),
                EndDate = new DateTime(2024, 12, 31),
                Status = ContractManagement.API.Models.ContractStatus.Active,
                ServiceLevel = ContractManagement.API.Models.ServiceLevel.Premium,
                SignedAgreementFileName = "ABC_Corp_Contract_2024.pdf",
                CreatedAt = DateTime.UtcNow
            },
            new ContractManagement.API.Models.Contract
            {
                Id = 2,
                ClientId = 2,
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2023, 12, 31),
                Status = ContractManagement.API.Models.ContractStatus.Expired,
                ServiceLevel = ContractManagement.API.Models.ServiceLevel.Basic,
                CreatedAt = DateTime.UtcNow
            },
            new ContractManagement.API.Models.Contract
            {
                Id = 3,
                ClientId = 1,
                StartDate = new DateTime(2024, 2, 1),
                EndDate = new DateTime(2025, 1, 31),
                Status = ContractManagement.API.Models.ContractStatus.Draft,
                ServiceLevel = ContractManagement.API.Models.ServiceLevel.Standard,
                CreatedAt = DateTime.UtcNow
            },
            new ContractManagement.API.Models.Contract
            {
                Id = 4,
                ClientId = 3,
                StartDate = new DateTime(2024, 1, 15),
                EndDate = new DateTime(2024, 12, 31),
                Status = ContractManagement.API.Models.ContractStatus.Active,
                ServiceLevel = ContractManagement.API.Models.ServiceLevel.Enterprise,
                SignedAgreementFileName = "TechSolutions_Enterprise_2024.pdf",
                CreatedAt = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<ContractManagement.API.Models.ServiceRequest>().HasData(
            new ContractManagement.API.Models.ServiceRequest
            {
                Id = 1,
                ContractId = 1,
                Description = "System integration support for ERP migration",
                Cost = 5000m,
                Status = ContractManagement.API.Models.RequestStatus.Completed,
                CreatedAt = DateTime.UtcNow.AddDays(-30),
                CompletedAt = DateTime.UtcNow.AddDays(-5)
            },
            new ContractManagement.API.Models.ServiceRequest
            {
                Id = 2,
                ContractId = 1,
                Description = "Security audit and penetration testing",
                Cost = 3500m,
                Status = ContractManagement.API.Models.RequestStatus.InProgress,
                CreatedAt = DateTime.UtcNow.AddDays(-15)
            },
            new ContractManagement.API.Models.ServiceRequest
            {
                Id = 3,
                ContractId = 1,
                Description = "24/7 monitoring setup",
                Cost = 2500m,
                Status = ContractManagement.API.Models.RequestStatus.Open,
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            },
            new ContractManagement.API.Models.ServiceRequest
            {
                Id = 4,
                ContractId = 4,
                Description = "Cloud migration assistance (AWS to Azure)",
                Cost = 15000m,
                Status = ContractManagement.API.Models.RequestStatus.Open,
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            },
            new ContractManagement.API.Models.ServiceRequest
            {
                Id = 5,
                ContractId = 4,
                Description = "DevOps pipeline implementation",
                Cost = 8000m,
                Status = ContractManagement.API.Models.RequestStatus.InProgress,
                CreatedAt = DateTime.UtcNow.AddDays(-7)
            }
        );
    }
}