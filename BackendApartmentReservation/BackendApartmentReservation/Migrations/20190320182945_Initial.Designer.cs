﻿// <auto-generated />
using System;
using BackendApartmentReservation.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackendApartmentReservation.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20190320182945_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbApartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.HasKey("Id");

                    b.ToTable("Apartments");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbEmployee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DbGroupId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Office");

                    b.HasKey("Id");

                    b.HasIndex("DbGroupId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ManagerId");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbTrip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Apartments");

                    b.Property<int>("CarRental");

                    b.Property<double>("Cost");

                    b.Property<DateTime>("DepartureDate");

                    b.Property<string>("DestinationOffice");

                    b.Property<DateTime>("ReturnDate");

                    b.Property<int>("TripTickets");

                    b.HasKey("Id");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbEmployee", b =>
                {
                    b.HasOne("BackendApartmentReservation.Database.Entities.DbGroup")
                        .WithMany("Employees")
                        .HasForeignKey("DbGroupId");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbGroup", b =>
                {
                    b.HasOne("BackendApartmentReservation.Database.Entities.DbEmployee", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId");
                });
#pragma warning restore 612, 618
        }
    }
}
