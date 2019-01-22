using Microsoft.EntityFrameworkCore.Migrations;

namespace SCJ.Booking.MVC.Migrations
{
    public partial class IncresedUserField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SmGovUserGuid",
                table: "BookingHistory",
                maxLength: 36);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
