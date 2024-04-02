using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    public partial class FairUseBookingRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScTrialBookingRequests",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    CeisPhysicalFileId = table.Column<decimal>(type: "numeric", nullable: false),
                    FullCaseNumber = table.Column<string>(
                        type: "character varying(20)",
                        maxLength: 20,
                        nullable: true
                    ),
                    StyleOfCause = table.Column<string>(
                        type: "character varying(255)",
                        maxLength: 255,
                        nullable: true
                    ),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    BookingLocationId = table.Column<int>(type: "integer", nullable: false),
                    TrialLocationName = table.Column<string>(type: "text", nullable: true),
                    FairUseBookingPeriodEndDate = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    FairUseBookingPeriodStartDate = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    FairUseContactDate = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    TrialPeriodStartDate = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    TrialPeriodEndDate = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    CourtClassCode = table.Column<string>(
                        type: "character varying(1)",
                        maxLength: 1,
                        nullable: true
                    ),
                    CourtClassName = table.Column<string>(
                        type: "character varying(30)",
                        maxLength: 30,
                        nullable: true
                    ),
                    BookHearingCode = table.Column<string>(
                        type: "character varying(10)",
                        maxLength: 10,
                        nullable: true
                    ),
                    HearingLength = table.Column<int>(type: "integer", nullable: false),
                    RequestedByName = table.Column<string>(
                        type: "character varying(40)",
                        maxLength: 40,
                        nullable: true
                    ),
                    Phone = table.Column<string>(
                        type: "character varying(20)",
                        maxLength: 20,
                        nullable: true
                    ),
                    Email = table.Column<string>(
                        type: "character varying(40)",
                        maxLength: 40,
                        nullable: true
                    ),
                    Timestamp = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScTrialBookingRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScTrialBookingRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "ScTrialDateSelections",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    BookingRequestId = table.Column<int>(type: "integer", nullable: true),
                    TrialStartDate = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    Rank = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScTrialDateSelections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScTrialDateSelections_ScTrialBookingRequests_BookingRequest~",
                        column: x => x.BookingRequestId,
                        principalTable: "ScTrialBookingRequests",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_ScTrialBookingRequests_UserId",
                table: "ScTrialBookingRequests",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ScTrialDateSelections_BookingRequestId",
                table: "ScTrialDateSelections",
                column: "BookingRequestId"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ScTrialDateSelections");

            migrationBuilder.DropTable(name: "ScTrialBookingRequests");
        }
    }
}
