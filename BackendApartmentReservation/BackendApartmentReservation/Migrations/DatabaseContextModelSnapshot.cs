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
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.Amenities.DbCarRentAmenity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset?>("BookedAt");

                    b.Property<int?>("CarReservationId");

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

                    b.HasKey("Id");

                    b.HasIndex("FlightReservationId");

                    b.ToTable("FlightAmenities");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.Amenities.DbLivingPlaceAmenity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApartmentRoomReservationId");

                    b.Property<DateTimeOffset?>("BookedAt");

                    b.Property<int?>("HotelReservationId");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentRoomReservationId");

                    b.HasIndex("HotelReservationId");

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

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbApartmentRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DbApartmentId");

                    b.Property<int>("RoomNumber");

                    b.HasKey("Id");

                    b.HasIndex("DbApartmentId");

                    b.ToTable("ApartmentRooms");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbAuthorization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<int?>("EmployeeId");

                    b.Property<string>("Password");

                    b.Property<string>("Salt");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Authorizations");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbEmployee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("ExternalEmployeeId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int?>("OfficeId");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.HasKey("Id");

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

                    b.Property<int?>("LivingPlaceId");

                    b.Property<int?>("TripId");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("FlightId");

                    b.HasIndex("LivingPlaceId");

                    b.HasIndex("TripId");

                    b.ToTable("Checklists");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbEmployeeGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DbEmployeeId");

                    b.Property<int?>("DbGroupId");

                    b.HasKey("Id");

                    b.HasIndex("DbEmployeeId");

                    b.HasIndex("DbGroupId");

                    b.ToTable("DbEmployeeGroup");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbEmployeePlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int?>("EmployeeId");

                    b.Property<DateTimeOffset>("EndDate");

                    b.Property<DateTimeOffset>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeePlans");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DbTripId");

                    b.Property<string>("ExternalGroupId");

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

                    b.Property<int>("DbApartmentId");

                    b.Property<string>("ExternalOfficeId");

                    b.HasKey("Id");

                    b.HasIndex("DbApartmentId");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbRoomReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateFrom");

                    b.Property<DateTime>("DateTo");

                    b.Property<int?>("EmployeeId");

                    b.Property<int?>("RoomId");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("RoomId");

                    b.ToTable("DbRoomReservations");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbTrip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DepartureDate");

                    b.Property<int?>("DestinationOfficeId");

                    b.Property<string>("ExternalTripId");

                    b.Property<DateTime>("ReturnDate");

                    b.Property<int?>("TripCreatorId");

                    b.HasKey("Id");

                    b.HasIndex("DestinationOfficeId");

                    b.HasIndex("TripCreatorId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.Reservations.DbCarReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CarAddress");

                    b.Property<string>("CarNumber");

                    b.Property<DateTime?>("RentEndTime");

                    b.Property<DateTime?>("RentStartTime");

                    b.HasKey("Id");

                    b.ToTable("CarReservations");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.Reservations.DbFlightReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AirportAddress");

                    b.Property<string>("Company");

                    b.Property<string>("FlightNumber");

                    b.Property<DateTime?>("FlightTime");

                    b.HasKey("Id");

                    b.ToTable("FlightReservations");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.Reservations.DbHotelReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateFrom");

                    b.Property<DateTime?>("DateTo");

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
                    b.HasOne("BackendApartmentReservation.Database.Entities.DbRoomReservation", "ApartmentRoomReservation")
                        .WithMany()
                        .HasForeignKey("ApartmentRoomReservationId");

                    b.HasOne("BackendApartmentReservation.Database.Entities.Reservations.DbHotelReservation", "HotelReservation")
                        .WithMany()
                        .HasForeignKey("HotelReservationId");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbApartmentRoom", b =>
                {
                    b.HasOne("BackendApartmentReservation.Database.Entities.DbApartment")
                        .WithMany("Rooms")
                        .HasForeignKey("DbApartmentId");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbAuthorization", b =>
                {
                    b.HasOne("BackendApartmentReservation.Database.Entities.DbEmployee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbEmployee", b =>
                {
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

                    b.HasOne("BackendApartmentReservation.Database.Entities.Amenities.DbLivingPlaceAmenity", "LivingPlace")
                        .WithMany()
                        .HasForeignKey("LivingPlaceId");

                    b.HasOne("BackendApartmentReservation.Database.Entities.DbTrip", "Trip")
                        .WithMany()
                        .HasForeignKey("TripId");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbEmployeeGroup", b =>
                {
                    b.HasOne("BackendApartmentReservation.Database.Entities.DbEmployee", "DbEmployee")
                        .WithMany()
                        .HasForeignKey("DbEmployeeId");

                    b.HasOne("BackendApartmentReservation.Database.Entities.DbGroup", "DbGroup")
                        .WithMany()
                        .HasForeignKey("DbGroupId");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbEmployeePlan", b =>
                {
                    b.HasOne("BackendApartmentReservation.Database.Entities.DbEmployee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");
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
                        .HasForeignKey("DbApartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbRoomReservation", b =>
                {
                    b.HasOne("BackendApartmentReservation.Database.Entities.DbEmployee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.HasOne("BackendApartmentReservation.Database.Entities.DbApartmentRoom", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId");
                });

            modelBuilder.Entity("BackendApartmentReservation.Database.Entities.DbTrip", b =>
                {
                    b.HasOne("BackendApartmentReservation.Database.Entities.DbOffice", "DestinationOffice")
                        .WithMany()
                        .HasForeignKey("DestinationOfficeId");

                    b.HasOne("BackendApartmentReservation.Database.Entities.DbEmployee", "TripCreator")
                        .WithMany()
                        .HasForeignKey("TripCreatorId");
                });
#pragma warning restore 612, 618
        }
    }
}
