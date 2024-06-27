using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    public partial class CascadeDeletes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScTrialDateSelections_ScTrialBookingRequests_BookingRequest~",
                table: "ScTrialDateSelections");

            migrationBuilder.AlterColumn<int>(
                name: "BookingRequestId",
                table: "ScTrialDateSelections",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScTrialDateSelections_ScTrialBookingRequests_BookingRequest~",
                table: "ScTrialDateSelections",
                column: "BookingRequestId",
                principalTable: "ScTrialBookingRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScTrialDateSelections_ScTrialBookingRequests_BookingRequest~",
                table: "ScTrialDateSelections");

            migrationBuilder.AlterColumn<int>(
                name: "BookingRequestId",
                table: "ScTrialDateSelections",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ScTrialDateSelections_ScTrialBookingRequests_BookingRequest~",
                table: "ScTrialDateSelections",
                column: "BookingRequestId",
                principalTable: "ScTrialBookingRequests",
                principalColumn: "Id");
        }
    }
}
