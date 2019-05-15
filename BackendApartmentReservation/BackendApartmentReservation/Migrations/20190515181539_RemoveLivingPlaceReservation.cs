using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class RemoveLivingPlaceReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivingPlaceAmenities_LivingPlaceReservations_LivingPlaceReservationId",
                table: "LivingPlaceAmenities");

            migrationBuilder.DropTable(
                name: "ApartmentAmenities");

            migrationBuilder.DropTable(
                name: "LivingPlaceReservations");

            migrationBuilder.RenameColumn(
                name: "LivingPlaceReservationId",
                table: "LivingPlaceAmenities",
                newName: "HotelReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_LivingPlaceAmenities_LivingPlaceReservationId",
                table: "LivingPlaceAmenities",
                newName: "IX_LivingPlaceAmenities_HotelReservationId");

            migrationBuilder.AddColumn<int>(
                name: "ApartmentRoomReservationId",
                table: "LivingPlaceAmenities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LivingPlaceAmenities_ApartmentRoomReservationId",
                table: "LivingPlaceAmenities",
                column: "ApartmentRoomReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LivingPlaceAmenities_DbRoomReservations_ApartmentRoomReservationId",
                table: "LivingPlaceAmenities",
                column: "ApartmentRoomReservationId",
                principalTable: "DbRoomReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LivingPlaceAmenities_HotelReservations_HotelReservationId",
                table: "LivingPlaceAmenities",
                column: "HotelReservationId",
                principalTable: "HotelReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivingPlaceAmenities_DbRoomReservations_ApartmentRoomReservationId",
                table: "LivingPlaceAmenities");

            migrationBuilder.DropForeignKey(
                name: "FK_LivingPlaceAmenities_HotelReservations_HotelReservationId",
                table: "LivingPlaceAmenities");

            migrationBuilder.DropIndex(
                name: "IX_LivingPlaceAmenities_ApartmentRoomReservationId",
                table: "LivingPlaceAmenities");

            migrationBuilder.DropColumn(
                name: "ApartmentRoomReservationId",
                table: "LivingPlaceAmenities");

            migrationBuilder.RenameColumn(
                name: "HotelReservationId",
                table: "LivingPlaceAmenities",
                newName: "LivingPlaceReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_LivingPlaceAmenities_HotelReservationId",
                table: "LivingPlaceAmenities",
                newName: "IX_LivingPlaceAmenities_LivingPlaceReservationId");

            migrationBuilder.CreateTable(
                name: "ApartmentAmenities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookedAt = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentAmenities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LivingPlaceReservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HotelReservationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivingPlaceReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LivingPlaceReservations_HotelReservations_HotelReservationId",
                        column: x => x.HotelReservationId,
                        principalTable: "HotelReservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
    }
}
