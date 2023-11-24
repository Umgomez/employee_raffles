using Microsoft.EntityFrameworkCore;

namespace employee_raffles.Models.Default;

public class EmployeesConfiguration
{
    [Obsolete]
    public static void Configure(ModelBuilder mb)
    {
        mb.Entity<Employees>(opt => {
            opt.ToTable("Empleados");
            opt.HasKey(x => x.EmpleadoID);
            opt.Property(x => x.Nombres)
              .HasMaxLength(50);
            opt.Property(x => x.Apellidos)
              .HasMaxLength(50);
            opt.Property(x => x.Cedula)
              .HasMaxLength(50);

            #region Constranints
            opt.HasIndex(x => new { x.Nombres, x.Apellidos, x.Cedula })
              .HasDatabaseName("UQ_Empleados")
              .IsUnique();
            opt.HasCheckConstraint("CHK_Empleados_Nombres", "Nombres <> ''");
            opt.HasCheckConstraint("CHK_Empleados_Apellidos", "Apellidos <> ''");
            opt.HasCheckConstraint("CHK_Empleados_Cedula", "Cedula <> ''");
            #endregion
        });
    }
}
