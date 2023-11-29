using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace employee_raffles.Migrations
{
    /// <inheritdoc />
    public partial class AddDateTime_Employees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAsistencia",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaAsistencia",
                table: "Employees");
        }
    }
}
