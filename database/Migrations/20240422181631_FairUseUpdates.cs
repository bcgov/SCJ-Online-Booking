using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    public partial class FairUseUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScTrialBookingRequests_Users_UserId",
                table: "ScTrialBookingRequests"
            );

            migrationBuilder.DropColumn(name: "CourtClassName", table: "ScTrialBookingRequests");

            migrationBuilder.DropColumn(name: "FullCaseNumber", table: "ScTrialBookingRequests");

            migrationBuilder.RenameColumn(
                name: "UnmetDemandMonths",
                table: "ScTrialBookingRequests",
                newName: "TrialLocationId"
            );

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "ScTrialBookingRequests",
                newName: "FairUseSort"
            );

            migrationBuilder.AddColumn<int>(
                name: "CaseNumber",
                table: "ScTrialBookingRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<string>(
                name: "CaseRegistryCode",
                table: "ScTrialBookingRequests",
                type: "character varying(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<int>(
                name: "CaseRegistryId",
                table: "ScTrialBookingRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddForeignKey(
                name: "FK_ScTrialBookingRequests_Users_UserId",
                table: "ScTrialBookingRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScTrialBookingRequests_Users_UserId",
                table: "ScTrialBookingRequests"
            );

            migrationBuilder.DropColumn(name: "CaseNumber", table: "ScTrialBookingRequests");

            migrationBuilder.DropColumn(name: "CaseRegistryCode", table: "ScTrialBookingRequests");

            migrationBuilder.DropColumn(name: "CaseRegistryId", table: "ScTrialBookingRequests");

            migrationBuilder.RenameColumn(
                name: "TrialLocationId",
                table: "ScTrialBookingRequests",
                newName: "UnmetDemandMonths"
            );

            migrationBuilder.RenameColumn(
                name: "FairUseSort",
                table: "ScTrialBookingRequests",
                newName: "LocationId"
            );

            migrationBuilder.AddColumn<string>(
                name: "CourtClassName",
                table: "ScTrialBookingRequests",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true
            );

            migrationBuilder.AddColumn<string>(
                name: "FullCaseNumber",
                table: "ScTrialBookingRequests",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true
            );
        }
    }
}
