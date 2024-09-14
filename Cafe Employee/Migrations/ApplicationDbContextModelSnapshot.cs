﻿// <auto-generated />
using System;
using Cafe_Employee.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cafe_Employee.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Cafe_Employee.Data.Models.Cafe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Cafes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("427a0f4f-4419-4e2d-817f-872a31441440"),
                            Description = "A cozy place for coffee lovers",
                            Location = "Downtown",
                            Logo = "",
                            Name = "Cafe Mocha"
                        },
                        new
                        {
                            Id = new Guid("b567aec5-270b-418b-a4d8-cd383275056b"),
                            Description = "Best lattes in town",
                            Location = "Uptown",
                            Logo = "",
                            Name = "Cafe Latte"
                        });
                });

            modelBuilder.Entity("Cafe_Employee.Data.Models.Employee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = "UI0000001",
                            EmailAddress = "john.doe@example.com",
                            Gender = "Male",
                            Name = "John Doe",
                            PhoneNumber = "91234567"
                        },
                        new
                        {
                            Id = "UI0000002",
                            EmailAddress = "jane.smith@example.com",
                            Gender = "Female",
                            Name = "Jane Smith",
                            PhoneNumber = "81234567"
                        });
                });

            modelBuilder.Entity("Cafe_Employee.Data.Models.EmployeeCafe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("CafeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CafeId");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("EmployeeCafes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CafeId = new Guid("427a0f4f-4419-4e2d-817f-872a31441440"),
                            EmployeeId = "UI0000001",
                            StartDate = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CafeId = new Guid("b567aec5-270b-418b-a4d8-cd383275056b"),
                            EmployeeId = "UI0000002",
                            StartDate = new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Cafe_Employee.Data.Models.EmployeeCafe", b =>
                {
                    b.HasOne("Cafe_Employee.Data.Models.Cafe", "Cafe")
                        .WithMany("EmployeeCafes")
                        .HasForeignKey("CafeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cafe_Employee.Data.Models.Employee", "Employee")
                        .WithMany("EmployeeCafes")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cafe");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Cafe_Employee.Data.Models.Cafe", b =>
                {
                    b.Navigation("EmployeeCafes");
                });

            modelBuilder.Entity("Cafe_Employee.Data.Models.Employee", b =>
                {
                    b.Navigation("EmployeeCafes");
                });
#pragma warning restore 612, 618
        }
    }
}
