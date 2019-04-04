using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class AddAllEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apartments",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "CarRental",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "DestinationOffice",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TripTickets",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "Office",
                table: "Employees",
                newName: "Email");

            migrationBuilder.AddColumn<int>(
                name: "DestinationOfficeId",
                table: "Trips",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DbTripId",
                table: "Groups",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartingOfficeId",
                table: "Groups",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "Employees",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "CarReservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CardPlateNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarReservations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlightReservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FlightNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightReservations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HotelReservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HotelName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelReservations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    OfficeApartmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offices_Apartments_OfficeApartmentId",
                        column: x => x.OfficeApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarRentAmenities",
                columns: table => new
                {
                    Required = table.Column<bool>(nullable: false),
                    BookedAt = table.Column<DateTimeOffset>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRentAmenities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarRentAmenities_CarReservations_CarId",
                        column: x => x.CarId,
                        principalTable: "CarReservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlightAmenities",
                columns: table => new
                {
                    Required = table.Column<bool>(nullable: false),
                    BookedAt = table.Column<DateTimeOffset>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReservationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightAmenities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightAmenities_FlightReservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "FlightReservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LivingPlaceAmenities",
                columns: table => new
                {
                    Required = table.Column<bool>(nullable: false),
                    BookedAt = table.Column<DateTimeOffset>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HotelId = table.Column<int>(nullable: true),
                    ApartmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivingPlaceAmenities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LivingPlaceAmenities_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LivingPlaceAmenities_HotelReservations_HotelId",
                        column: x => x.HotelId,
                        principalTable: "HotelReservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Checklists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    LivingPlaceId = table.Column<int>(nullable: true),
                    CarId = table.Column<int>(nullable: true),
                    FlightId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checklists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checklists_CarRentAmenities_CarId",
                        column: x => x.CarId,
                        principalTable: "CarRentAmenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Checklists_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Checklists_FlightAmenities_FlightId",
                        column: x => x.FlightId,
                        principalTable: "FlightAmenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Checklists_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Checklists_LivingPlaceAmenities_LivingPlaceId",
                        column: x => x.LivingPlaceId,
                        principalTable: "LivingPlaceAmenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DestinationOfficeId",
                table: "Trips",
                column: "DestinationOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_DbTripId",
                table: "Groups",
                column: "DbTripId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_StartingOfficeId",
                table: "Groups",
                column: "StartingOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_OfficeId",
                table: "Employees",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentAmenities_CarId",
                table: "CarRentAmenities",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_CarId",
                table: "Checklists",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_EmployeeId",
                table: "Checklists",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_FlightId",
                table: "Checklists",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_GroupId",
                table: "Checklists",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_LivingPlaceId",
                table: "Checklists",
                column: "LivingPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightAmenities_ReservationId",
                table: "FlightAmenities",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_LivingPlaceAmenities_ApartmentId",
                table: "LivingPlaceAmenities",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LivingPlaceAmenities_HotelId",
                table: "LivingPlaceAmenities",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_OfficeApartmentId",
                table: "Offices",
                column: "OfficeApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Offices_OfficeId",
                table: "Employees",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Trips_DbTripId",
                table: "Groups",
                column: "DbTripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Offices_StartingOfficeId",
                table: "Groups",
                column: "StartingOfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Offices_DestinationOfficeId",
                table: "Trips",
                column: "DestinationOfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Offices_OfficeId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Trips_DbTripId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Offices_StartingOfficeId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Offices_DestinationOfficeId",
                table: "Trips");

            migrationBuilder.DropTable(
                name: "Checklists");

            migrationBuilder.DropTable(
                name: "Offices");

            migrationBuilder.DropTable(
                name: "CarRentAmenities");

            migrationBuilder.DropTable(
                name: "FlightAmenities");

            migrationBuilder.DropTable(
                name: "LivingPlaceAmenities");

            migrationBuilder.DropTable(
                name: "CarReservations");

            migrationBuilder.DropTable(
                name: "FlightReservations");

            migrationBuilder.DropTable(
                name: "HotelReservations");

            migrationBuilder.DropIndex(
                name: "IX_Trips_DestinationOfficeId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Groups_DbTripId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_StartingOfficeId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Employees_OfficeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DestinationOfficeId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "DbTripId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "StartingOfficeId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Employees",
                newName: "Office");

            migrationBuilder.AddColumn<int>(
                name: "Apartments",
                table: "Trips",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CarRental",
                table: "Trips",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DestinationOffice",
                table: "Trips",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TripTickets",
                table: "Trips",
                nullable: false,
                defaultValue: 0);
        }
    }
}
