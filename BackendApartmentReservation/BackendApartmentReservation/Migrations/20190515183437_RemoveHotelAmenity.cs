using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class RemoveHotelAmenity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelAmenities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HotelAmenities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookedAt = table.Column<DateTimeOffset>(nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_HotelAmenities_HotelReservationId",
                table: "HotelAmenities",
                column: "HotelReservationId");
        }
    }
}
