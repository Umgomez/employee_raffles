using Microsoft.EntityFrameworkCore;

namespace employee_raffles.Models.Default;

public class EmployeesConfiguration
{
    public static void Configure(ModelBuilder mb)
    {
        mb.Entity<Employees>(opt => {
            opt.ToTable("Employees");
            opt.HasKey(x => x.EmpleadoID);
            opt.Property(x => x.Tarjeta)
              .HasMaxLength(10);
            opt.Property(x => x.Nombres)
              .HasMaxLength(50);
            opt.Property(x => x.Apellidos)
              .HasMaxLength(50);
            opt.Property(x => x.Cedula)
              .HasMaxLength(50);

            #region Constranints
            opt.HasIndex(x => new { x.Tarjeta, x.Nombres, x.Apellidos, x.Cedula })
              .HasDatabaseName("UQ_Employees")
              .IsUnique();
            opt.HasCheckConstraint("CHK_Employees_Tarjeta", "Tarjeta <> ''");
            opt.HasCheckConstraint("CHK_Employees_Nombres", "Nombres <> ''");
            opt.HasCheckConstraint("CHK_Employees_Apellidos", "Apellidos <> ''");
            opt.HasCheckConstraint("CHK_Employees_Cedula", "Cedula <> ''");
            #endregion
        });
    }
}
