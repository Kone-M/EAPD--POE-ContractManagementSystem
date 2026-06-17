using ContractManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ContractManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Client)
                .WithMany(c => c.Contracts)
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Contract)
                .WithMany(c => c.ServiceRequests)
                .HasForeignKey(sr => sr.ContractId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure indexes for better performance
            modelBuilder.Entity<Contract>()
                .HasIndex(c => c.Status);

            modelBuilder.Entity<Contract>()
                .HasIndex(c => c.ClientId);

            modelBuilder.Entity<ServiceRequest>()
                .HasIndex(sr => sr.ContractId);

            modelBuilder.Entity<ServiceRequest>()
                .HasIndex(sr => sr.Status);

            modelBuilder.Entity<Client>()
                .HasIndex(c => c.Email)
                .IsUnique();

            // Configure decimal precision
            modelBuilder.Entity<ServiceRequest>()
                .Property(sr => sr.Cost)
                .HasPrecision(18, 2);

            // Seed initial data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Clients
            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = 1,
                    Name = "ABC Corporation",
                    Email = "contact@abccorp.com",
                    Phone = "+1 (555) 123-4567",
                    Address = "123 Business Avenue, New York, NY 10001",
                    Region = "North America",
                    CreatedAt = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc)
                },
                new Client
                {
                    Id = 2,
                    Name = "XYZ Enterprises",
                    Email = "info@xyzent.com",
                    Phone = "+44 20 1234 5678",
                    Address = "456 Commerce Street, London, UK",
                    Region = "Europe",
                    CreatedAt = new DateTime(2024, 1, 20, 11, 0, 0, DateTimeKind.Utc)
                },
                new Client
                {
                    Id = 3,
                    Name = "Tech Solutions Ltd",
                    Email = "sales@techsolutions.com",
                    Phone = "+61 2 9876 5432",
                    Address = "789 Innovation Drive, Sydney, Australia",
                    Region = "Asia Pacific",
                    CreatedAt = new DateTime(2024, 1, 25, 12, 0, 0, DateTimeKind.Utc)
                },
                new Client
                {
                    Id = 4,
                    Name = "Global Industries Inc",
                    Email = "info@globalindustries.com",
                    Phone = "+1 (555) 987-6543",
                    Address = "321 Global Plaza, Chicago, IL 60601",
                    Region = "North America",
                    CreatedAt = new DateTime(2024, 2, 1, 13, 0, 0, DateTimeKind.Utc)
                }
            );

            // Seed Contracts
            modelBuilder.Entity<Contract>().HasData(
                new Contract
                {
                    Id = 1,
                    ClientId = 1,
                    StartDate = new DateTime(2024, 1, 1),
                    EndDate = new DateTime(2024, 12, 31),
                    Status = ContractStatus.Active,
                    ServiceLevel = ServiceLevel.Premium,
                    SignedAgreementFileName = "ABC_Corp_Contract_2024.pdf",
                    CreatedAt = new DateTime(2024, 1, 15, 14, 0, 0, DateTimeKind.Utc)
                },
                new Contract
                {
                    Id = 2,
                    ClientId = 2,
                    StartDate = new DateTime(2023, 1, 1),
                    EndDate = new DateTime(2023, 12, 31),
                    Status = ContractStatus.Expired,
                    ServiceLevel = ServiceLevel.Basic,
                    CreatedAt = new DateTime(2023, 1, 1, 9, 0, 0, DateTimeKind.Utc)
                },
                new Contract
                {
                    Id = 3,
                    ClientId = 1,
                    StartDate = new DateTime(2024, 2, 1),
                    EndDate = new DateTime(2025, 1, 31),
                    Status = ContractStatus.Draft,
                    ServiceLevel = ServiceLevel.Standard,
                    CreatedAt = new DateTime(2024, 2, 1, 10, 0, 0, DateTimeKind.Utc)
                },
                new Contract
                {
                    Id = 4,
                    ClientId = 3,
                    StartDate = new DateTime(2024, 1, 15),
                    EndDate = new DateTime(2024, 12, 31),
                    Status = ContractStatus.Active,
                    ServiceLevel = ServiceLevel.Enterprise,
                    SignedAgreementFileName = "TechSolutions_Enterprise_2024.pdf",
                    CreatedAt = new DateTime(2024, 1, 15, 15, 0, 0, DateTimeKind.Utc)
                },
                new Contract
                {
                    Id = 5,
                    ClientId = 4,
                    StartDate = new DateTime(2024, 2, 1),
                    EndDate = new DateTime(2024, 8, 31),
                    Status = ContractStatus.OnHold,
                    ServiceLevel = ServiceLevel.Standard,
                    CreatedAt = new DateTime(2024, 2, 1, 11, 0, 0, DateTimeKind.Utc)
                }
            );

            // Seed Service Requests
            modelBuilder.Entity<ServiceRequest>().HasData(
                new ServiceRequest
                {
                    Id = 1,
                    ContractId = 1,
                    Description = "System integration support for ERP migration",
                    Cost = 5000m,
                    Status = RequestStatus.Completed,
                    CreatedAt = new DateTime(2024, 1, 20, 9, 0, 0, DateTimeKind.Utc),
                    CompletedAt = new DateTime(2024, 2, 10, 17, 0, 0, DateTimeKind.Utc)
                },
                new ServiceRequest
                {
                    Id = 2,
                    ContractId = 1,
                    Description = "Security audit and penetration testing",
                    Cost = 3500m,
                    Status = RequestStatus.InProgress,
                    CreatedAt = new DateTime(2024, 2, 1, 10, 0, 0, DateTimeKind.Utc),
                    CompletedAt = null
                },
                new ServiceRequest
                {
                    Id = 3,
                    ContractId = 1,
                    Description = "24/7 monitoring setup",
                    Cost = 2500m,
                    Status = RequestStatus.Open,
                    CreatedAt = new DateTime(2024, 2, 15, 14, 0, 0, DateTimeKind.Utc),
                    CompletedAt = null
                },
                new ServiceRequest
                {
                    Id = 4,
                    ContractId = 4,
                    Description = "Cloud migration assistance (AWS to Azure)",
                    Cost = 15000m,
                    Status = RequestStatus.Open,
                    CreatedAt = new DateTime(2024, 1, 20, 11, 0, 0, DateTimeKind.Utc),
                    CompletedAt = null
                },
                new ServiceRequest
                {
                    Id = 5,
                    ContractId = 4,
                    Description = "DevOps pipeline implementation",
                    Cost = 8000m,
                    Status = RequestStatus.InProgress,
                    CreatedAt = new DateTime(2024, 2, 1, 13, 0, 0, DateTimeKind.Utc),
                    CompletedAt = null
                }
            );
        }
    }
}