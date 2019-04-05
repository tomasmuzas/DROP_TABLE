﻿// <auto-generated />
using System;
using BackendApartmentReservation.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackendApartmentReservation.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.Amenities.DbCarRentAmenity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset?>("BookedAt");

                    b.Property<int?>("CarReservationId");

                    b.Property<bool>("Required");

                    b.HasKey("Id");

                    b.HasIndex("CarReservationId");

                    b.ToTable("CarRentAmenities");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.Amenities.DbFlightAmenity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset?>("BookedAt");

                    b.Property<int?>("FlightReservationId");

                    b.Property<bool>("Required");

                    b.HasKey("Id");

                    b.HasIndex("FlightReservationId");

                    b.ToTable("FlightAmenities");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.Amenities.DbLivingPlaceAmenity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApartmentId");

                    b.Property<DateTimeOffset?>("BookedAt");

                    b.Property<int?>("HotelId");

                    b.Property<bool>("Required");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.HasIndex("HotelId");

                    b.ToTable("LivingPlaceAmenities");
                });

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

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int?>("OfficeId");

                    b.HasKey("Id");

                    b.HasIndex("DbGroupId");

                    b.HasIndex("OfficeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbEmployeeAmenitiesChecklist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CarId");

                    b.Property<int?>("EmployeeId");

                    b.Property<int?>("FlightId");

                    b.Property<int?>("GroupId");

                    b.Property<int?>("LivingPlaceId");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("FlightId");

                    b.HasIndex("GroupId");

                    b.HasIndex("LivingPlaceId");

                    b.ToTable("Checklists");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DbTripId");

                    b.Property<int?>("ManagerId");

                    b.Property<int?>("StartingOfficeId");

                    b.HasKey("Id");

                    b.HasIndex("DbTripId");

                    b.HasIndex("ManagerId");

                    b.HasIndex("StartingOfficeId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbOffice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<int?>("OfficeApartmentId");

                    b.HasKey("Id");

                    b.HasIndex("OfficeApartmentId");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbTrip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cost");

                    b.Property<DateTime>("DepartureDate");

                    b.Property<int?>("DestinationOfficeId");

                    b.Property<DateTime>("ReturnDate");

                    b.HasKey("Id");

                    b.HasIndex("DestinationOfficeId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.Reservations.DbCarReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CarNumber");

                    b.HasKey("Id");

                    b.ToTable("CarReservations");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.Reservations.DbFlightReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FlightNumber");

                    b.HasKey("Id");

                    b.ToTable("FlightReservations");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.Reservations.DbHotelReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HotelName");

                    b.HasKey("Id");

                    b.ToTable("HotelReservations");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.Amenities.DbCarRentAmenity", b =>
                {
                    b.HasOne("BackendApartmentReservation.Database.Entities.Reservations.DbCarReservation", "CarReservation")
                        .WithMany()
                        .HasForeignKey("CarReservationId");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.Amenities.DbFlightAmenity", b =>
                {
                    b.HasOne("BackendApartmentReservation.Database.Entities.Reservations.DbFlightReservation", "FlightReservation")
                        .WithMany()
                        .HasForeignKey("FlightReservationId");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.Amenities.DbLivingPlaceAmenity", b =>
                {
                    b.HasOne("BackendApartmentReservation.Database.Entities.DbApartment", "Apartment")
                        .WithMany()
                        .HasForeignKey("ApartmentId");

                    b.HasOne("BackendApartmentReservation.Database.Entities.Reservations.DbHotelReservation", "Hotel")
                        .WithMany()
                        .HasForeignKey("HotelId");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbEmployee", b =>
                {
                    b.HasOne("BackendApartmentReservation.Database.Entities.DbGroup")
                        .WithMany("Employees")
                        .HasForeignKey("DbGroupId");

                    b.HasOne("BackendApartmentReservation.Database.Entities.DbOffice", "Office")
                        .WithMany()
                        .HasForeignKey("OfficeId");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbEmployeeAmenitiesChecklist", b =>
                {
                    b.HasOne("BackendApartmentReservation.Database.Entities.Amenities.DbCarRentAmenity", "Car")
                        .WithMany()
                        .HasForeignKey("CarId");

                    b.HasOne("BackendApartmentReservation.Database.Entities.DbEmployee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.HasOne("BackendApartmentReservation.Database.Entities.Amenities.DbFlightAmenity", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId");

                    b.HasOne("BackendApartmentReservation.Database.Entities.DbGroup", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.HasOne("BackendApartmentReservation.Database.Entities.Amenities.DbLivingPlaceAmenity", "LivingPlace")
                        .WithMany()
                        .HasForeignKey("LivingPlaceId");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbGroup", b =>
                {
                    b.HasOne("BackendApartmentReservation.Database.Entities.DbTrip")
                        .WithMany("Groups")
                        .HasForeignKey("DbTripId");

                    b.HasOne("BackendApartmentReservation.Database.Entities.DbEmployee", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId");

                    b.HasOne("BackendApartmentReservation.Database.Entities.DbOffice", "StartingOffice")
                        .WithMany()
                        .HasForeignKey("StartingOfficeId");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbOffice", b =>
                {
                    b.HasOne("BackendApartmentReservation.Database.Entities.DbApartment", "OfficeApartment")
                        .WithMany()
                        .HasForeignKey("OfficeApartmentId");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbTrip", b =>
                {
                    b.HasOne("BackendApartmentReservation.Database.Entities.DbOffice", "DestinationOffice")
                        .WithMany()
                        .HasForeignKey("DestinationOfficeId");
                });
#pragma warning restore 612, 618
        }
    }
}
