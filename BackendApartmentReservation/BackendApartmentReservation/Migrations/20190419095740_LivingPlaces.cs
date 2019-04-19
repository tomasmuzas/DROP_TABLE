using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class LivingPlaces : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivingPlaceAmenities_Apartments_ApartmentId",
                table: "LivingPlaceAmenities");

            migrationBuilder.DropForeignKey(
                name: "FK_LivingPlaceAmenities_HotelReservations_HotelId",
                table: "LivingPlaceAmenities");

            migrationBuilder.DropIndex(
                name: "IX_LivingPlaceAmenities_ApartmentId",
                table: "LivingPlaceAmenities");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "LivingPlaceAmenities");

            migrationBuilder.RenameColumn(
                name: "HotelId",
                table: "LivingPlaceAmenities",
                newName: "LivingPlaceReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_LivingPlaceAmenities_HotelId",
                table: "LivingPlaceAmenities",
                newName: "IX_LivingPlaceAmenities_LivingPlaceReservationId");

            migrationBuilder.RenameColumn(
                name: "HotelName",
                table: "HotelReservations",
                newName: "Address");

            migrationBuilder.CreateTable(
                name: "ApartmentReservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentReservations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HotelAmenities",
                columns: table => new
                {
                    BookedAt = table.Column<DateTimeOffset>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HotelReservationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelAmenities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelAmenities_HotelReservations_HotelReservationId",
                        column: x => x.HotelReservationId,
                        principalTable: "HotelReservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApartmentAmenities",
                columns: table => new
                {
                    BookedAt = table.Column<DateTimeOffset>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApartmentReservationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentAmenities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApartmentAmenities_ApartmentReservations_ApartmentReservationId",
                        column: x => x.ApartmentReservationId,
                        principalTable: "ApartmentReservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LivingPlaceReservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApartmentReservationId = table.Column<int>(nullable: true),
                    HotelReservationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivingPlaceReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LivingPlaceReservations_ApartmentReservations_ApartmentReservationId",
                        column: x => x.ApartmentReservationId,
                        principalTable: "ApartmentReservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LivingPlaceReservations_HotelReservations_HotelReservationId",
                        column: x => x.HotelReservationId,
                        principalTable: "HotelReservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentAmenities_ApartmentReservationId",
                table: "ApartmentAmenities",
                column: "ApartmentReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelAmenities_HotelReservationId",
                table: "HotelAmenities",
                column: "HotelReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_LivingPlaceReservations_ApartmentReservationId",
                table: "LivingPlaceReservations",
                column: "ApartmentReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_LivingPlaceReservations_HotelReservationId",
                table: "LivingPlaceReservations",
                column: "HotelReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LivingPlaceAmenities_LivingPlaceReservations_LivingPlaceReservationId",
                table: "LivingPlaceAmenities",
                column: "LivingPlaceReservationId",
                principalTable: "LivingPlaceReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivingPlaceAmenities_LivingPlaceReservations_LivingPlaceReservationId",
                table: "LivingPlaceAmenities");

            migrationBuilder.DropTable(
                name: "ApartmentAmenities");

            migrationBuilder.DropTable(
                name: "HotelAmenities");

            migrationBuilder.DropTable(
                name: "LivingPlaceReservations");

            migrationBuilder.DropTable(
                name: "ApartmentReservations");

            migrationBuilder.RenameColumn(
                name: "LivingPlaceReservationId",
                table: "LivingPlaceAmenities",
                newName: "HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_LivingPlaceAmenities_LivingPlaceReservationId",
                table: "LivingPlaceAmenities",
                newName: "IX_LivingPlaceAmenities_HotelId");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "HotelReservations",
                newName: "HotelName");

            migrationBuilder.AddColumn<int>(
                name: "ApartmentId",
                table: "LivingPlaceAmenities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LivingPlaceAmenities_ApartmentId",
                table: "LivingPlaceAmenities",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_LivingPlaceAmenities_Apartments_ApartmentId",
                table: "LivingPlaceAmenities",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LivingPlaceAmenities_HotelReservations_HotelId",
                table: "LivingPlaceAmenities",
                column: "HotelId",
                principalTable: "HotelReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
