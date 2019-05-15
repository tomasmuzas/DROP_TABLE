using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class RemoveApartmentReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentAmenities_ApartmentReservations_ApartmentReservationId",
                table: "ApartmentAmenities");

            migrationBuilder.DropForeignKey(
                name: "FK_LivingPlaceReservations_ApartmentReservations_ApartmentReservationId",
                table: "LivingPlaceReservations");

            migrationBuilder.DropTable(
                name: "ApartmentReservations");

            migrationBuilder.DropIndex(
                name: "IX_LivingPlaceReservations_ApartmentReservationId",
                table: "LivingPlaceReservations");

            migrationBuilder.DropIndex(
                name: "IX_ApartmentAmenities_ApartmentReservationId",
                table: "ApartmentAmenities");

            migrationBuilder.DropColumn(
                name: "ApartmentReservationId",
                table: "LivingPlaceReservations");

            migrationBuilder.DropColumn(
                name: "ApartmentReservationId",
                table: "ApartmentAmenities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApartmentReservationId",
                table: "LivingPlaceReservations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApartmentReservationId",
                table: "ApartmentAmenities",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_LivingPlaceReservations_ApartmentReservationId",
                table: "LivingPlaceReservations",
                column: "ApartmentReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentAmenities_ApartmentReservationId",
                table: "ApartmentAmenities",
                column: "ApartmentReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentAmenities_ApartmentReservations_ApartmentReservationId",
                table: "ApartmentAmenities",
                column: "ApartmentReservationId",
                principalTable: "ApartmentReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LivingPlaceReservations_ApartmentReservations_ApartmentReservationId",
                table: "LivingPlaceReservations",
                column: "ApartmentReservationId",
                principalTable: "ApartmentReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
