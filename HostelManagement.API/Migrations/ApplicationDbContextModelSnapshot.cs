﻿// <auto-generated />
using System;
using HostelManagement.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HostelManagement.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HostelManagement.DAL.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DurationOfStay")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("HostelManagement.DAL.Models.Hostel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("NoOfAvailableRooms")
                        .HasColumnType("int");

                    b.Property<int>("NoOfRooms")
                        .HasColumnType("int");

                    b.Property<int>("NoOfStudents")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Hostels");
                });

            modelBuilder.Entity("HostelManagement.DAL.Models.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MealType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("HostelManagement.DAL.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(15, 2)
                        .HasColumnType("decimal(15,2)");

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<string>("ModeOfPayment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("HostelManagement.DAL.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FloorNo")
                        .HasColumnType("int");

                    b.Property<int>("HostelId")
                        .HasColumnType("int");

                    b.Property<string>("RoomStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HostelId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HostelManagement.DAL.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuardianName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuardianPhno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LaundryServices")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MealServices")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("HostelManagement.DAL.Models.Booking", b =>
                {
                    b.HasOne("HostelManagement.DAL.Models.Student", "students")
                        .WithMany("bookings")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("students");
                });

            modelBuilder.Entity("HostelManagement.DAL.Models.Meal", b =>
                {
                    b.HasOne("HostelManagement.DAL.Models.Student", "students")
                        .WithMany("meals")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("students");
                });

            modelBuilder.Entity("HostelManagement.DAL.Models.Payment", b =>
                {
                    b.HasOne("HostelManagement.DAL.Models.Booking", "bookings")
                        .WithMany("payments")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("bookings");
                });

            modelBuilder.Entity("HostelManagement.DAL.Models.Room", b =>
                {
                    b.HasOne("HostelManagement.DAL.Models.Hostel", "hostels")
                        .WithMany("rooms")
                        .HasForeignKey("HostelId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("hostels");
                });

            modelBuilder.Entity("HostelManagement.DAL.Models.Student", b =>
                {
                    b.HasOne("HostelManagement.DAL.Models.Room", "rooms")
                        .WithMany("students")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("rooms");
                });

            modelBuilder.Entity("HostelManagement.DAL.Models.Booking", b =>
                {
                    b.Navigation("payments");
                });

            modelBuilder.Entity("HostelManagement.DAL.Models.Hostel", b =>
                {
                    b.Navigation("rooms");
                });

            modelBuilder.Entity("HostelManagement.DAL.Models.Room", b =>
                {
                    b.Navigation("students");
                });

            modelBuilder.Entity("HostelManagement.DAL.Models.Student", b =>
                {
                    b.Navigation("bookings");

                    b.Navigation("meals");
                });
#pragma warning restore 612, 618
        }
    }
}
