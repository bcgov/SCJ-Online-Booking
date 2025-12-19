using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChambersHearingSubType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LongChambersHearingSubTypeId",
                table: "ScLotteryBookingRequests",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LongChambersHearingSubTypeName",
                table: "ScLotteryBookingRequests",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongChambersHearingSubTypeId",
                table: "ScLotteryBookingRequests");

            migrationBuilder.DropColumn(
                name: "LongChambersHearingSubTypeName",
                table: "ScLotteryBookingRequests");
        }
    }
}
