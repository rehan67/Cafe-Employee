using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cafe_Employee.Migrations
{
    /// <inheritdoc />
    public partial class MakeLogoNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cafes",
                keyColumn: "Id",
                keyValue: new Guid("03e39c13-efe7-4492-9913-1fec4f1217fb"));

            migrationBuilder.DeleteData(
                table: "Cafes",
                keyColumn: "Id",
                keyValue: new Guid("eabbfc55-dc34-40ca-98dd-f3addf3cc60b"));

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "Cafes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Cafes",
                columns: new[] { "Id", "Description", "Location", "Logo", "Name" },
                values: new object[,]
                {
                    { new Guid("427a0f4f-4419-4e2d-817f-872a31441440"), "A cozy place for coffee lovers", "Downtown", "", "Cafe Mocha" },
                    { new Guid("b567aec5-270b-418b-a4d8-cd383275056b"), "Best lattes in town", "Uptown", "", "Cafe Latte" }
                });

            migrationBuilder.UpdateData(
                table: "EmployeeCafes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CafeId",
                value: new Guid("427a0f4f-4419-4e2d-817f-872a31441440"));

            migrationBuilder.UpdateData(
                table: "EmployeeCafes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CafeId",
                value: new Guid("b567aec5-270b-418b-a4d8-cd383275056b"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cafes",
                keyColumn: "Id",
                keyValue: new Guid("427a0f4f-4419-4e2d-817f-872a31441440"));

            migrationBuilder.DeleteData(
                table: "Cafes",
                keyColumn: "Id",
                keyValue: new Guid("b567aec5-270b-418b-a4d8-cd383275056b"));

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "Cafes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Cafes",
                columns: new[] { "Id", "Description", "Location", "Logo", "Name" },
                values: new object[,]
                {
                    { new Guid("03e39c13-efe7-4492-9913-1fec4f1217fb"), "Best lattes in town", "Uptown", "", "Cafe Latte" },
                    { new Guid("eabbfc55-dc34-40ca-98dd-f3addf3cc60b"), "A cozy place for coffee lovers", "Downtown", "", "Cafe Mocha" }
                });

            migrationBuilder.UpdateData(
                table: "EmployeeCafes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CafeId",
                value: new Guid("eabbfc55-dc34-40ca-98dd-f3addf3cc60b"));

            migrationBuilder.UpdateData(
                table: "EmployeeCafes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CafeId",
                value: new Guid("03e39c13-efe7-4492-9913-1fec4f1217fb"));
        }
    }
}
