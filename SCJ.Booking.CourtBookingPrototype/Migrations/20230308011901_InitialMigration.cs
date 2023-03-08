using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCJ.Booking.CourtBookingPrototype.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CaseBookingRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SmGovUserGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhysicalFileId = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BookingPeriodId = table.Column<int>(type: "int", nullable: false),
                    TrialLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HearingType = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseBookingRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistrySettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistryId = table.Column<int>(type: "int", nullable: false),
                    UsesLottery = table.Column<bool>(type: "bit", nullable: false),
                    MaximumDateSelections = table.Column<int>(type: "int", nullable: false),
                    MonthlyBookingWeek = table.Column<int>(type: "int", nullable: false),
                    MonthlyBookingDay = table.Column<int>(type: "int", nullable: false),
                    BookingEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BookingStartTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrySettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DateSelections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseBookingRequestId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PreferenceOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateSelections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DateSelections_CaseBookingRequests_CaseBookingRequestId",
                        column: x => x.CaseBookingRequestId,
                        principalTable: "CaseBookingRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistryId = table.Column<int>(type: "int", nullable: false),
                    OpeningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrySettingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingPeriods_RegistrySettings_RegistrySettingId",
                        column: x => x.RegistrySettingId,
                        principalTable: "RegistrySettings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingPeriods_RegistrySettingId",
                table: "BookingPeriods",
                column: "RegistrySettingId");

            migrationBuilder.CreateIndex(
                name: "IX_DateSelections_CaseBookingRequestId",
                table: "DateSelections",
                column: "CaseBookingRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingPeriods");

            migrationBuilder.DropTable(
                name: "DateSelections");

            migrationBuilder.DropTable(
                name: "RegistrySettings");

            migrationBuilder.DropTable(
                name: "CaseBookingRequests");
        }
    }
}
