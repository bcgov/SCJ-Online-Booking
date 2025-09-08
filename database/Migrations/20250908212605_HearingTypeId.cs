using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    /// <inheritdoc />
    public partial class HearingTypeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaskRunner_Lottery",
                table: "ScLotteryBookingRequests");

            migrationBuilder.AddColumn<int>(
                name: "HearingTypeId",
                table: "ScLotteryBookingRequests",
                type: "integer",
                nullable: false,
                defaultValue: 9001);

            migrationBuilder.AddColumn<int>(
                name: "HearingTypeId",
                table: "ScLotteries",
                type: "integer",
                nullable: false,
                defaultValue: 9001);

            migrationBuilder.CreateIndex(
                name: "IX_TaskRunner_Lottery",
                table: "ScLotteryBookingRequests",
                columns: new[] { "LotteryStartDate", "BookingLocationId", "HearingTypeId", "BookHearingCode" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaskRunner_Lottery",
                table: "ScLotteryBookingRequests");

            migrationBuilder.DropColumn(
                name: "HearingTypeId",
                table: "ScLotteryBookingRequests");

            migrationBuilder.DropColumn(
                name: "HearingTypeId",
                table: "ScLotteries");

            migrationBuilder.CreateIndex(
                name: "IX_TaskRunner_Lottery",
                table: "ScLotteryBookingRequests",
                columns: new[] { "LotteryStartDate", "BookingLocationId", "BookHearingCode" });
        }
    }
}
