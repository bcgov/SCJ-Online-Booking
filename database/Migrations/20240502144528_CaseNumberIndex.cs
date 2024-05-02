using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    public partial class CaseNumberIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CaseNumber_Ascending",
                table: "ScTrialBookingRequests",
                column: "CaseNumber"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CaseNumber_Ascending",
                table: "ScTrialBookingRequests"
            );
        }
    }
}
