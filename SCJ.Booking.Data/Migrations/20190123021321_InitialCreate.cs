using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SCJ.Booking.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingHistory",
                columns: table => new
                {
                    SmGovUserGuid = table.Column<string>(maxLength: 36, nullable: false),
                    ContainerId = table.Column<long>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingHistory", x => new { x.SmGovUserGuid, x.ContainerId, x.Timestamp });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingHistory");
        }
    }
}
