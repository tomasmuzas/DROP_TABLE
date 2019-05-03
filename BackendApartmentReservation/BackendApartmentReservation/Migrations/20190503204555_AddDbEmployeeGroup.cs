using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApartmentReservation.Migrations
{
    public partial class AddDbEmployeeGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbEmployeeGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DbEmployeeId = table.Column<int>(nullable: true),
                    DbGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbEmployeeGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbEmployeeGroup_Employees_DbEmployeeId",
                        column: x => x.DbEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DbEmployeeGroup_Groups_DbGroupId",
                        column: x => x.DbGroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbEmployeeGroup_DbEmployeeId",
                table: "DbEmployeeGroup",
                column: "DbEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DbEmployeeGroup_DbGroupId",
                table: "DbEmployeeGroup",
                column: "DbGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbEmployeeGroup");
        }
    }
}
