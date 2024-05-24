using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    public partial class Lottery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllocatedSelectionTrialStartDate",
                table: "ScTrialBookingRequests");

            migrationBuilder.DropColumn(
                name: "LotteryBeginTimestamp",
                table: "ScTrialBookingRequests");

            migrationBuilder.AddColumn<string>(
                name: "BookingResult",
                table: "ScTrialDateSelections",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LotteryId",
                table: "ScTrialBookingRequests",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnmetDemandBookingResult",
                table: "ScTrialBookingRequests",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ScLotteries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    BookingLocationId = table.Column<int>(type: "integer", nullable: false),
                    BookHearingCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    FairUseBookingPeriodEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FairUseBookingPeriodStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    InitiationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScLotteries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScTrialBookingRequests_LotteryId",
                table: "ScTrialBookingRequests",
                column: "LotteryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScTrialBookingRequests_ScLotteries_LotteryId",
                table: "ScTrialBookingRequests",
                column: "LotteryId",
                principalTable: "ScLotteries",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScTrialBookingRequests_ScLotteries_LotteryId",
                table: "ScTrialBookingRequests");

            migrationBuilder.DropTable(
                name: "ScLotteries");

            migrationBuilder.DropIndex(
                name: "IX_ScTrialBookingRequests_LotteryId",
                table: "ScTrialBookingRequests");

            migrationBuilder.DropColumn(
                name: "BookingResult",
                table: "ScTrialDateSelections");

            migrationBuilder.DropColumn(
                name: "LotteryId",
                table: "ScTrialBookingRequests");

            migrationBuilder.DropColumn(
                name: "UnmetDemandBookingResult",
                table: "ScTrialBookingRequests");

            migrationBuilder.AddColumn<DateTime>(
                name: "AllocatedSelectionTrialStartDate",
                table: "ScTrialBookingRequests",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LotteryBeginTimestamp",
                table: "ScTrialBookingRequests",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
