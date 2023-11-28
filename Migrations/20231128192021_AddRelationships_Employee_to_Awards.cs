using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace employee_raffles.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationships_Employee_to_Awards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AwardsId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AwardsId",
                table: "Employees",
                column: "AwardsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Awards_AwardsId",
                table: "Employees",
                column: "AwardsId",
                principalTable: "Awards",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Awards_AwardsId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_AwardsId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AwardsId",
                table: "Employees");
        }
    }
}
