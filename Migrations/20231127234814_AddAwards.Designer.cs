﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using employee_raffles.Data;

#nullable disable

namespace employee_raffles.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231127234814_AddAwards")]
    partial class AddAwards
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("employee_raffles.Models.Default.Awards", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Amount")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("IsSelected")
                        .HasColumnType("bit");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Sequence", "Amount")
                        .IsUnique()
                        .HasDatabaseName("UQ_Awards")
                        .HasFilter("[Amount] IS NOT NULL");

                    b.ToTable("Awards", null, t =>
                        {
                            t.HasCheckConstraint("CHK_Awards_Amount", "Amount <> ''");

                            t.HasCheckConstraint("CHK_Awards_Sequence", "Sequence <> 0");
                        });
                });

            modelBuilder.Entity("employee_raffles.Models.Default.Employees", b =>
                {
                    b.Property<int>("EmpleadoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmpleadoID"));

                    b.Property<string>("Apellidos")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Asistencia")
                        .HasColumnType("bit");

                    b.Property<string>("Cedula")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nombres")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("NumeroRifa")
                        .HasColumnType("int");

                    b.Property<bool>("SelRifa")
                        .HasColumnType("bit");

                    b.Property<string>("Tarjeta")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("EmpleadoID");

                    b.HasIndex("Tarjeta", "Nombres", "Apellidos", "Cedula")
                        .IsUnique()
                        .HasDatabaseName("UQ_Employees")
                        .HasFilter("[Tarjeta] IS NOT NULL AND [Nombres] IS NOT NULL AND [Apellidos] IS NOT NULL AND [Cedula] IS NOT NULL");

                    b.ToTable("Employees", null, t =>
                        {
                            t.HasCheckConstraint("CHK_Employees_Apellidos", "Apellidos <> ''");

                            t.HasCheckConstraint("CHK_Employees_Cedula", "Cedula <> ''");

                            t.HasCheckConstraint("CHK_Employees_Nombres", "Nombres <> ''");

                            t.HasCheckConstraint("CHK_Employees_Tarjeta", "Tarjeta <> ''");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
