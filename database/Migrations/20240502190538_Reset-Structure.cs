using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    public partial class ResetStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "BookingHistory");

            migrationBuilder.DropTable(name: "DataProtectionKeys");

            migrationBuilder.DropTable(name: "EmailQueue");

            migrationBuilder.DropTable(name: "ScTrialDateSelections");

            migrationBuilder.DropTable(name: "ScTrialBookingRequests");

            migrationBuilder.DropTable(name: "Users");

            migrationBuilder.CreateTable(
                name: "DataProtectionKeys",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        )
                        .Annotation("Sqlite:Autoincrement", true),
                    FriendlyName = table.Column<string>(type: "text", nullable: true),
                    Xml = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataProtectionKeys", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "EmailQueue",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        )
                        .Annotation("Sqlite:Autoincrement", true),
                    CourtLevel = table.Column<string>(type: "text", nullable: false),
                    ToEmail = table.Column<string>(type: "text", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    TimeStamp = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailQueue", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        )
                        .Annotation("Sqlite:Autoincrement", true),
                    CredentialType = table.Column<int>(type: "integer", nullable: false),
                    UniqueIdentifier = table.Column<string>(type: "text", nullable: false),
                    LastLogin = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "BookingHistory",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        )
                        .Annotation("Sqlite:Autoincrement", true),
                    Timestamp = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    BookingLocationName = table.Column<string>(
                        type: "character varying(20)",
                        maxLength: 20,
                        nullable: false
                    ),
                    CourtLevel = table.Column<string>(
                        type: "character varying(3)",
                        maxLength: 3,
                        nullable: false
                    ),
                    CoaCaseType = table.Column<string>(
                        type: "character varying(8)",
                        maxLength: 8,
                        nullable: true
                    ),
                    CoaConferenceType = table.Column<string>(
                        type: "character varying(8)",
                        maxLength: 8,
                        nullable: true
                    ),
                    ScHearingType = table.Column<int>(type: "integer", nullable: true),
                    ScFormulaType = table.Column<string>(
                        type: "character varying(8)",
                        maxLength: 8,
                        nullable: true
                    ),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingHistory_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "ScTrialBookingRequests",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        )
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    CaseRegistryId = table.Column<int>(type: "integer", nullable: false),
                    CaseRegistryCode = table.Column<string>(
                        type: "character varying(2)",
                        maxLength: 2,
                        nullable: false
                    ),
                    CaseNumber = table.Column<int>(type: "integer", nullable: false),
                    CeisPhysicalFileId = table.Column<decimal>(type: "numeric", nullable: false),
                    StyleOfCause = table.Column<string>(
                        type: "character varying(255)",
                        maxLength: 255,
                        nullable: false
                    ),
                    TrialLocationId = table.Column<int>(type: "integer", nullable: false),
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
                        nullable: false
                    ),
                    BookHearingCode = table.Column<string>(
                        type: "character varying(10)",
                        maxLength: 10,
                        nullable: false
                    ),
                    HearingLength = table.Column<int>(type: "integer", nullable: false),
                    FairUseSort = table.Column<int>(type: "integer", nullable: false),
                    RequestedByName = table.Column<string>(
                        type: "character varying(40)",
                        maxLength: 40,
                        nullable: false
                    ),
                    Phone = table.Column<string>(
                        type: "character varying(20)",
                        maxLength: 20,
                        nullable: false
                    ),
                    Email = table.Column<string>(
                        type: "character varying(40)",
                        maxLength: 40,
                        nullable: false
                    ),
                    CreationTimestamp = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    TrialBookingId = table.Column<string>(type: "text", nullable: false),
                    LotteryBeginTimestamp = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    ),
                    LotteryPosition = table.Column<int>(type: "integer", nullable: false),
                    IsProcessed = table.Column<bool>(type: "boolean", nullable: false),
                    AllocatedSelectionTrialStartDate = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    ),
                    AllocatedSelectionRank = table.Column<int>(type: "integer", nullable: false)
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
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        )
                        .Annotation("Sqlite:Autoincrement", true),
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
                name: "IX_BookingHistory_UserId",
                table: "BookingHistory",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_CaseNumber_Ascending",
                table: "ScTrialBookingRequests",
                column: "CaseNumber"
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

            migrationBuilder.CreateIndex(
                name: "IX_Users_UniqueIdentifier_CredentialType",
                table: "Users",
                columns: new[] { "UniqueIdentifier", "CredentialType" },
                unique: true
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "BookingHistory");

            migrationBuilder.DropTable(name: "DataProtectionKeys");

            migrationBuilder.DropTable(name: "EmailQueue");

            migrationBuilder.DropTable(name: "ScTrialDateSelections");

            migrationBuilder.DropTable(name: "ScTrialBookingRequests");

            migrationBuilder.DropTable(name: "Users");
        }
    }
}
