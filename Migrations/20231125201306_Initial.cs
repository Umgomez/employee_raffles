using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace employee_raffles.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmpleadoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tarjeta = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Asistencia = table.Column<bool>(type: "bit", nullable: false),
                    NumeroRifa = table.Column<int>(type: "int", nullable: false),
                    SelRifa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmpleadoID);
                    table.CheckConstraint("CHK_Employees_Apellidos", "Apellidos <> ''");
                    table.CheckConstraint("CHK_Employees_Cedula", "Cedula <> ''");
                    table.CheckConstraint("CHK_Employees_Nombres", "Nombres <> ''");
                    table.CheckConstraint("CHK_Employees_Tarjeta", "Tarjeta <> ''");
                });

            migrationBuilder.CreateIndex(
                name: "UQ_Employees",
                table: "Employees",
                columns: new[] { "Tarjeta", "Nombres", "Apellidos", "Cedula" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
