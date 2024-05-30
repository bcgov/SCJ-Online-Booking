using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    public partial class LotteryStartDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FairUseContactDate",
                table: "ScTrialBookingRequests",
                newName: "LotteryStartDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProcessingTimestamp",
                table: "ScTrialBookingRequests",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LotteryStartDate",
                table: "ScTrialBookingRequests",
                newName: "FairUseContactDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProcessingTimestamp",
                table: "ScTrialBookingRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }
    }
}
