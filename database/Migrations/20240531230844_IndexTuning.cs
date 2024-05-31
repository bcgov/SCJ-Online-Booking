using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    public partial class IndexTuning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LotteryStartDate_Ascending",
                table: "ScTrialBookingRequests",
                column: "LotteryStartDate");

            migrationBuilder.CreateIndex(
                name: "IX_TaskRunner_Lottery",
                table: "ScTrialBookingRequests",
                columns: new[] { "LotteryStartDate", "BookingLocationId", "BookHearingCode" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LotteryStartDate_Ascending",
                table: "ScTrialBookingRequests");

            migrationBuilder.DropIndex(
                name: "IX_TaskRunner_Lottery",
                table: "ScTrialBookingRequests");
        }
    }
}
