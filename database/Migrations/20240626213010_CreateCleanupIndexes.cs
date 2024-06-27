using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    public partial class CreateCleanupIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProcessingTimestamp_Ascending",
                table: "ScTrialBookingRequests",
                column: "ProcessingTimestamp");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessingTimestamp_Email_RequestedByName_Ascending",
                table: "ScTrialBookingRequests",
                columns: new[] { "ProcessingTimestamp", "Email", "RequestedByName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProcessingTimestamp_Ascending",
                table: "ScTrialBookingRequests");

            migrationBuilder.DropIndex(
                name: "IX_ProcessingTimestamp_Email_RequestedByName_Ascending",
                table: "ScTrialBookingRequests");
        }
    }
}
