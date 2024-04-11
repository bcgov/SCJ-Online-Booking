using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    public partial class AdditionalFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookingLocationName",
                table: "BookingHistory",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "ScTrialBookingRequests",
                newName: "CreationTimestamp"
            );

            migrationBuilder.AddColumn<int>(
                name: "AllocatedSelectionRank",
                table: "ScTrialBookingRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<DateTime>(
                name: "AllocatedSelectionTrialStartDate",
                table: "ScTrialBookingRequests",
                type: "timestamp with time zone",
                nullable: true
            );

            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "ScTrialBookingRequests",
                type: "boolean",
                nullable: false,
                defaultValue: false
            );

            migrationBuilder.AddColumn<DateTime>(
                name: "LotteryBeginTimestamp",
                table: "ScTrialBookingRequests",
                type: "timestamp with time zone",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "LotteryPosition",
                table: "ScTrialBookingRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<int>(
                name: "UnmetDemandMonths",
                table: "ScTrialBookingRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Timestamp", table: "ScTrialBookingRequests");

            migrationBuilder.DropColumn(
                name: "AllocatedSelectionRank",
                table: "ScTrialBookingRequests"
            );

            migrationBuilder.DropColumn(
                name: "AllocatedSelectionTrialStartDate",
                table: "ScTrialBookingRequests"
            );

            migrationBuilder.DropColumn(name: "IsProcessed", table: "ScTrialBookingRequests");

            migrationBuilder.DropColumn(
                name: "LotteryBeginTimestamp",
                table: "ScTrialBookingRequests"
            );

            migrationBuilder.DropColumn(name: "LotteryPosition", table: "ScTrialBookingRequests");

            migrationBuilder.DropColumn(name: "UnmetDemandMonths", table: "ScTrialBookingRequests");

            migrationBuilder.RenameColumn(
                name: "CreationTimestamp",
                table: "ScTrialBookingRequests",
                newName: "Timestamp"
            );

            migrationBuilder.DropColumn(name: "BookingLocationName", table: "BookingHistory");
        }
    }
}
