﻿using employee_raffles.Models.Default;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace employee_raffles.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    #region Default
    public DbSet<Employees> Employees { get; set; }
    public DbSet<Awards> Awards { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Default
        EmployeesConfiguration.Configure(modelBuilder);
        AwardsConfiguration.Configure(modelBuilder);
        #endregion

        base.OnModelCreating(modelBuilder);

        foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
    }
}
