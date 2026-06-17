using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContractManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ServiceLevel = table.Column<int>(type: "int", nullable: false),
                    SignedAgreementPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignedAgreementFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "CreatedAt", "Email", "Name", "Phone", "Region" },
                values: new object[,]
                {
                    { 1, "123 Business Avenue, New York, NY 10001", new DateTime(2026, 6, 17, 13, 51, 28, 120, DateTimeKind.Utc).AddTicks(2593), "contact@abccorp.com", "ABC Corporation", "+1 (555) 123-4567", "North America" },
                    { 2, "456 Commerce Street, London, UK", new DateTime(2026, 6, 17, 13, 51, 28, 120, DateTimeKind.Utc).AddTicks(2597), "info@xyzent.com", "XYZ Enterprises", "+44 20 1234 5678", "Europe" },
                    { 3, "789 Innovation Drive, Sydney, Australia", new DateTime(2026, 6, 17, 13, 51, 28, 120, DateTimeKind.Utc).AddTicks(2600), "sales@techsolutions.com", "Tech Solutions Ltd", "+61 2 9876 5432", "Asia Pacific" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 6, 17, 13, 51, 27, 944, DateTimeKind.Utc).AddTicks(8466), "$2a$11$fSFIKzhboNYQNpvCLvCaEuPKQZGak23OSr0Z4sblvdKVE/MXfiOMu", "Admin", "admin" },
                    { 2, new DateTime(2026, 6, 17, 13, 51, 28, 120, DateTimeKind.Utc).AddTicks(1402), "$2a$11$EWyd7acwzVzlKiypgTJVH.hj.wVg.YEc.jgmQnmc9fhgf0nc4pE62", "Manager", "manager" }
                });

            migrationBuilder.InsertData(
                table: "Contracts",
                columns: new[] { "Id", "ClientId", "CreatedAt", "EndDate", "ServiceLevel", "SignedAgreementFileName", "SignedAgreementPath", "StartDate", "Status" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 6, 17, 13, 51, 28, 120, DateTimeKind.Utc).AddTicks(2656), new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "ABC_Corp_Contract_2024.pdf", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 2, new DateTime(2026, 6, 17, 13, 51, 28, 120, DateTimeKind.Utc).AddTicks(2661), new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, 1, new DateTime(2026, 6, 17, 13, 51, 28, 120, DateTimeKind.Utc).AddTicks(2664), new DateTime(2025, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, null, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 4, 3, new DateTime(2026, 6, 17, 13, 51, 28, 120, DateTimeKind.Utc).AddTicks(2666), new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "TechSolutions_Enterprise_2024.pdf", null, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "ServiceRequests",
                columns: new[] { "Id", "CompletedAt", "ContractId", "Cost", "CreatedAt", "Description", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 6, 12, 13, 51, 28, 120, DateTimeKind.Utc).AddTicks(2947), 1, 5000m, new DateTime(2026, 5, 18, 13, 51, 28, 120, DateTimeKind.Utc).AddTicks(2935), "System integration support for ERP migration", 2 },
                    { 2, null, 1, 3500m, new DateTime(2026, 6, 2, 13, 51, 28, 120, DateTimeKind.Utc).AddTicks(2955), "Security audit and penetration testing", 1 },
                    { 3, null, 1, 2500m, new DateTime(2026, 6, 12, 13, 51, 28, 120, DateTimeKind.Utc).AddTicks(2957), "24/7 monitoring setup", 0 },
                    { 4, null, 4, 15000m, new DateTime(2026, 6, 7, 13, 51, 28, 120, DateTimeKind.Utc).AddTicks(2960), "Cloud migration assistance (AWS to Azure)", 0 },
                    { 5, null, 4, 8000m, new DateTime(2026, 6, 10, 13, 51, 28, 120, DateTimeKind.Utc).AddTicks(2962), "DevOps pipeline implementation", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Email",
                table: "Clients",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ClientId",
                table: "Contracts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_Status",
                table: "Contracts",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_ContractId",
                table: "ServiceRequests",
                column: "ContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceRequests");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
