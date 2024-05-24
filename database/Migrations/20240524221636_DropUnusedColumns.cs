using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    public partial class DropUnusedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrialPeriodEndDate",
                table: "ScTrialBookingRequests");

            migrationBuilder.DropColumn(
                name: "TrialPeriodStartDate",
                table: "ScTrialBookingRequests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TrialPeriodEndDate",
                table: "ScTrialBookingRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TrialPeriodStartDate",
                table: "ScTrialBookingRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
