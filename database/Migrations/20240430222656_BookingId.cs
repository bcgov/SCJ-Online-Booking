using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    public partial class BookingId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StyleOfCause",
                table: "ScTrialBookingRequests",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "RequestedByName",
                table: "ScTrialBookingRequests",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "ScTrialBookingRequests",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ScTrialBookingRequests",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "CourtClassCode",
                table: "ScTrialBookingRequests",
                type: "character varying(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(1)",
                oldMaxLength: 1,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "BookHearingCode",
                table: "ScTrialBookingRequests",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10,
                oldNullable: true
            );

            migrationBuilder.AddColumn<string>(
                name: "TrialBookingId",
                table: "ScTrialBookingRequests",
                type: "text",
                nullable: false,
                defaultValue: ""
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "TrialBookingId", table: "ScTrialBookingRequests");

            migrationBuilder.AlterColumn<string>(
                name: "StyleOfCause",
                table: "ScTrialBookingRequests",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255
            );

            migrationBuilder.AlterColumn<string>(
                name: "RequestedByName",
                table: "ScTrialBookingRequests",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40
            );

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "ScTrialBookingRequests",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20
            );

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ScTrialBookingRequests",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40
            );

            migrationBuilder.AlterColumn<string>(
                name: "CourtClassCode",
                table: "ScTrialBookingRequests",
                type: "character varying(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1)",
                oldMaxLength: 1
            );

            migrationBuilder.AlterColumn<string>(
                name: "BookHearingCode",
                table: "ScTrialBookingRequests",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10
            );
        }
    }
}
