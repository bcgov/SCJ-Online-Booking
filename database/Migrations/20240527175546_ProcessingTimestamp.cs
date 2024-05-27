using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    public partial class ProcessingTimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ProcessingTimestamp",
                table: "ScTrialBookingRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessingTimestamp",
                table: "ScTrialBookingRequests");
        }
    }
}
