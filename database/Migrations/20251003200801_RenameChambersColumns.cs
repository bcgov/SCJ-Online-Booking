using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameChambersColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TrialLocationName",
                table: "ScLotteryBookingRequests",
                newName: "LocationName");

            migrationBuilder.RenameColumn(
                name: "TrialLocationId",
                table: "ScLotteryBookingRequests",
                newName: "LocationId");

            migrationBuilder.RenameColumn(
                name: "TrialBookingId",
                table: "ScLotteryBookingRequests",
                newName: "LotteryEntryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LotteryEntryId",
                table: "ScLotteryBookingRequests",
                newName: "TrialBookingId");

            migrationBuilder.RenameColumn(
                name: "LocationName",
                table: "ScLotteryBookingRequests",
                newName: "TrialLocationName");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "ScLotteryBookingRequests",
                newName: "TrialLocationId");
        }
    }
}
