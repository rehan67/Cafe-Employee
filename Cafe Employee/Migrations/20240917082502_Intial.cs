using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cafe_Employee.Migrations
{
    /// <inheritdoc />
    public partial class Intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cafes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cafes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeCafes",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CafeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCafes", x => new { x.EmployeeId, x.CafeId });
                    table.ForeignKey(
                        name: "FK_EmployeeCafes_Cafes_CafeId",
                        column: x => x.CafeId,
                        principalTable: "Cafes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeCafes_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cafes",
                columns: new[] { "Id", "Description", "Location", "Logo", "Name" },
                values: new object[,]
                {
                    { new Guid("c3fd6065-7247-4263-9695-6198936ba13d"), "Best lattes in town", "Uptown", "", "Cafe Latte" },
                    { new Guid("ed23ecc1-eb67-44be-8629-dddcd0496a79"), "A cozy place for coffee lovers", "Downtown", "", "Cafe Mocha" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "EmailAddress", "Gender", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { "UI0000001", "john.doe@example.com", "Male", "John Doe", "91234567" },
                    { "UI0000002", "jane.smith@example.com", "Female", "Jane Smith", "81234567" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeCafes",
                columns: new[] { "CafeId", "EmployeeId", "Id", "StartDate" },
                values: new object[,]
                {
                    { new Guid("ed23ecc1-eb67-44be-8629-dddcd0496a79"), "UI0000001", 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c3fd6065-7247-4263-9695-6198936ba13d"), "UI0000002", 2, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCafes_CafeId",
                table: "EmployeeCafes",
                column: "CafeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCafes_EmployeeId",
                table: "EmployeeCafes",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmailAddress",
                table: "Employees",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PhoneNumber",
                table: "Employees",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeCafes");

            migrationBuilder.DropTable(
                name: "Cafes");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
