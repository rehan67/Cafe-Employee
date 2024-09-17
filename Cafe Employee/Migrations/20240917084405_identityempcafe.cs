using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cafe_Employee.Migrations
{
    /// <inheritdoc />
    public partial class identityempcafe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeCafes",
                keyColumns: new[] { "CafeId", "EmployeeId" },
                keyValues: new object[] { new Guid("ed23ecc1-eb67-44be-8629-dddcd0496a79"), "UI0000001" });

            migrationBuilder.DeleteData(
                table: "EmployeeCafes",
                keyColumns: new[] { "CafeId", "EmployeeId" },
                keyValues: new object[] { new Guid("c3fd6065-7247-4263-9695-6198936ba13d"), "UI0000002" });

            migrationBuilder.DeleteData(
                table: "Cafes",
                keyColumn: "Id",
                keyValue: new Guid("c3fd6065-7247-4263-9695-6198936ba13d"));

            migrationBuilder.DeleteData(
                table: "Cafes",
                keyColumn: "Id",
                keyValue: new Guid("ed23ecc1-eb67-44be-8629-dddcd0496a79"));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "EmployeeCafes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.InsertData(
                table: "Cafes",
                columns: new[] { "Id", "Description", "Location", "Logo", "Name" },
                values: new object[,]
                {
                    { new Guid("ab7ddd79-4768-4e10-9230-a21a5832b159"), "A cozy place for coffee lovers", "Downtown", "", "Cafe Mocha" },
                    { new Guid("ba3f1bb8-23ef-421b-a084-40b40f6afc2a"), "Best lattes in town", "Uptown", "", "Cafe Latte" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeCafes",
                columns: new[] { "CafeId", "EmployeeId", "Id", "StartDate" },
                values: new object[,]
                {
                    { new Guid("ab7ddd79-4768-4e10-9230-a21a5832b159"), "UI0000001", 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("ba3f1bb8-23ef-421b-a084-40b40f6afc2a"), "UI0000002", 2, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeCafes",
                keyColumns: new[] { "CafeId", "EmployeeId" },
                keyValues: new object[] { new Guid("ab7ddd79-4768-4e10-9230-a21a5832b159"), "UI0000001" });

            migrationBuilder.DeleteData(
                table: "EmployeeCafes",
                keyColumns: new[] { "CafeId", "EmployeeId" },
                keyValues: new object[] { new Guid("ba3f1bb8-23ef-421b-a084-40b40f6afc2a"), "UI0000002" });

            migrationBuilder.DeleteData(
                table: "Cafes",
                keyColumn: "Id",
                keyValue: new Guid("ab7ddd79-4768-4e10-9230-a21a5832b159"));

            migrationBuilder.DeleteData(
                table: "Cafes",
                keyColumn: "Id",
                keyValue: new Guid("ba3f1bb8-23ef-421b-a084-40b40f6afc2a"));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "EmployeeCafes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.InsertData(
                table: "Cafes",
                columns: new[] { "Id", "Description", "Location", "Logo", "Name" },
                values: new object[,]
                {
                    { new Guid("c3fd6065-7247-4263-9695-6198936ba13d"), "Best lattes in town", "Uptown", "", "Cafe Latte" },
                    { new Guid("ed23ecc1-eb67-44be-8629-dddcd0496a79"), "A cozy place for coffee lovers", "Downtown", "", "Cafe Mocha" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeCafes",
                columns: new[] { "CafeId", "EmployeeId", "Id", "StartDate" },
                values: new object[,]
                {
                    { new Guid("ed23ecc1-eb67-44be-8629-dddcd0496a79"), "UI0000001", 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c3fd6065-7247-4263-9695-6198936ba13d"), "UI0000002", 2, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }
    }
}
