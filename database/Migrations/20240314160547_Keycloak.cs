using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    public partial class Keycloak : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"BookingHistory\"", true);

            migrationBuilder.DropPrimaryKey(name: "PK_BookingHistory", table: "BookingHistory");

            migrationBuilder.DropColumn(name: "SmGovUserGuid", table: "BookingHistory");

            migrationBuilder.DropColumn(name: "ContainerId", table: "BookingHistory");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "BookingHistory",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone"
            );

            migrationBuilder
                .AddColumn<long>(
                    name: "Id",
                    table: "BookingHistory",
                    type: "integer",
                    nullable: false,
                    defaultValue: 0L
                )
                .Annotation("Sqlite:Autoincrement", true)
                .Annotation(
                    "Npgsql:ValueGenerationStrategy",
                    NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                );

            migrationBuilder.AddColumn<string>(
                name: "CoaCaseType",
                table: "BookingHistory",
                type: "text",
                maxLength: 8,
                nullable: true
            );

            migrationBuilder.AddColumn<string>(
                name: "CoaConferenceType",
                table: "BookingHistory",
                type: "text",
                maxLength: 8,
                nullable: true
            );

            migrationBuilder.AddColumn<string>(
                name: "CourtLevel",
                table: "BookingHistory",
                type: "text",
                maxLength: 3,
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<string>(
                name: "ScFormulaType",
                table: "BookingHistory",
                type: "text",
                maxLength: 8,
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "ScHearingType",
                table: "BookingHistory",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "BookingHistory",
                type: "integer",
                nullable: false,
                defaultValue: 0L
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingHistory",
                table: "BookingHistory",
                column: "Id"
            );

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table
                        .Column<long>(type: "integer", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CredentialType = table.Column<ushort>(type: "integer", nullable: false),
                    UniqueIdentifier = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_BookingHistory_UserId",
                table: "BookingHistory",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Users_UniqueIdentifier_CredentialType",
                table: "Users",
                columns: new[] { "UniqueIdentifier", "CredentialType" },
                unique: true
            );

            migrationBuilder.AddForeignKey(
                name: "FK_BookingHistory_Users_UserId",
                table: "BookingHistory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingHistory_Users_UserId",
                table: "BookingHistory"
            );

            migrationBuilder.DropTable(name: "Users");

            migrationBuilder.DropPrimaryKey(name: "PK_BookingHistory", table: "BookingHistory");

            migrationBuilder.DropIndex(name: "IX_BookingHistory_UserId", table: "BookingHistory");

            migrationBuilder.DropColumn(name: "Id", table: "BookingHistory");

            migrationBuilder.DropColumn(name: "CoaCaseType", table: "BookingHistory");

            migrationBuilder.DropColumn(name: "CoaConferenceType", table: "BookingHistory");

            migrationBuilder.DropColumn(name: "CourtLevel", table: "BookingHistory");

            migrationBuilder.DropColumn(name: "ScFormulaType", table: "BookingHistory");

            migrationBuilder.DropColumn(name: "ScHearingType", table: "BookingHistory");

            migrationBuilder.DropColumn(name: "UserId", table: "BookingHistory");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "BookingHistory",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "text"
            );

            migrationBuilder.AddColumn<string>(
                name: "SmGovUserGuid",
                table: "BookingHistory",
                type: "character varying(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<long>(
                name: "ContainerId",
                table: "BookingHistory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingHistory",
                table: "BookingHistory",
                columns: new[] { "SmGovUserGuid", "ContainerId", "Timestamp" }
            );
        }
    }
}
