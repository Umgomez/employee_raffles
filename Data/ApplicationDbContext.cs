using employee_raffles.Models.Default;
using Microsoft.EntityFrameworkCore;

namespace employee_raffles.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    #region Default
    public DbSet<Employees> Employees { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Default
        EmployeesConfiguration.Configure(modelBuilder);
        #endregion

        base.OnModelCreating(modelBuilder);

        foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
    }
}
