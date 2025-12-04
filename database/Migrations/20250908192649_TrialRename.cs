using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCJ.Booking.Data.Migrations
{
    /// <inheritdoc />
    public partial class TrialRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop foreign keys from ScTrialDateSelections and ScTrialBookingRequests
            migrationBuilder.DropForeignKey(
                name: "FK_ScTrialDateSelections_ScTrialBookingRequests_BookingRequest~",
                table: "ScTrialDateSelections"
            );

            // Drop foreign keys from ScTrialBookingRequests
            migrationBuilder.DropForeignKey(
                name: "FK_ScTrialBookingRequests_ScLotteries_LotteryId",
                table: "ScTrialBookingRequests"
            );
            migrationBuilder.DropForeignKey(
                name: "FK_ScTrialBookingRequests_Users_UserId",
                table: "ScTrialBookingRequests"
            );

            // Drop old PKs
            migrationBuilder.DropPrimaryKey(
                name: "PK_ScTrialBookingRequests",
                table: "ScTrialBookingRequests"
            );
            migrationBuilder.DropPrimaryKey(
                name: "PK_ScTrialDateSelections",
                table: "ScTrialDateSelections"
            );

            // Rename tables
            migrationBuilder.RenameTable(
                name: "ScTrialBookingRequests",
                newName: "ScLotteryBookingRequests"
            );
            migrationBuilder.RenameTable(
                name: "ScTrialDateSelections",
                newName: "ScLotteryDateSelections"
            );

            // Rename indexes for ScTrialBookingRequests -> ScLotteryBookingRequests
            migrationBuilder.RenameIndex(
                name: "IX_ScTrialBookingRequests_LotteryId",
                table: "ScLotteryBookingRequests",
                newName: "IX_ScLotteryBookingRequests_LotteryId"
            );
            migrationBuilder.RenameIndex(
                name: "IX_ScTrialBookingRequests_UserId",
                table: "ScLotteryBookingRequests",
                newName: "IX_ScLotteryBookingRequests_UserId"
            );
            migrationBuilder.RenameIndex(
                name: "IX_CaseNumber_Ascending",
                table: "ScLotteryBookingRequests",
                newName: "IX_CaseNumber_Ascending"
            );
            migrationBuilder.RenameIndex(
                name: "IX_LotteryStartDate_Ascending",
                table: "ScLotteryBookingRequests",
                newName: "IX_LotteryStartDate_Ascending"
            );
            migrationBuilder.RenameIndex(
                name: "IX_ProcessingTimestamp_Ascending",
                table: "ScLotteryBookingRequests",
                newName: "IX_ProcessingTimestamp_Ascending"
            );
            migrationBuilder.RenameIndex(
                name: "IX_ProcessingTimestamp_Email_RequestedByName_Ascending",
                table: "ScLotteryBookingRequests",
                newName: "IX_ProcessingTimestamp_Email_RequestedByName_Ascending"
            );
            migrationBuilder.RenameIndex(
                name: "IX_TaskRunner_Lottery",
                table: "ScLotteryBookingRequests",
                newName: "IX_TaskRunner_Lottery"
            );

            // Rename indexes for ScTrialDateSelections -> ScLotteryDateSelections
            migrationBuilder.RenameIndex(
                name: "IX_ScTrialDateSelections_BookingRequestId",
                table: "ScLotteryDateSelections",
                newName: "IX_ScLotteryDateSelections_BookingRequestId"
            );

            // Add new PKs
            migrationBuilder.AddPrimaryKey(
                name: "PK_ScLotteryBookingRequests",
                table: "ScLotteryBookingRequests",
                column: "Id"
            );
            migrationBuilder.AddPrimaryKey(
                name: "PK_ScLotteryDateSelections",
                table: "ScLotteryDateSelections",
                column: "Id"
            );

            // Add foreign keys back with new names
            migrationBuilder.AddForeignKey(
                name: "FK_ScLotteryDateSelections_ScLotteryBookingRequests_BookingRequestId",
                table: "ScLotteryDateSelections",
                column: "BookingRequestId",
                principalTable: "ScLotteryBookingRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
            migrationBuilder.AddForeignKey(
                name: "FK_ScLotteryBookingRequests_ScLotteries_LotteryId",
                table: "ScLotteryBookingRequests",
                column: "LotteryId",
                principalTable: "ScLotteries",
                principalColumn: "Id"
            );
            migrationBuilder.AddForeignKey(
                name: "FK_ScLotteryBookingRequests_Users_UserId",
                table: "ScLotteryBookingRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key from ScTrialDateSelections to ScTrialBookingRequests
            migrationBuilder.DropForeignKey(
                name: "FK_ScLotteryDateSelections_ScLotteryBookingRequests_BookingRequestId",
                table: "ScLotteryDateSelections"
            );

            // Drop foreign keys from ScLotteryBookingRequests
            migrationBuilder.DropForeignKey(
                name: "FK_ScLotteryBookingRequests_ScLotteries_LotteryId",
                table: "ScLotteryBookingRequests"
            );
            migrationBuilder.DropForeignKey(
                name: "FK_ScLotteryBookingRequests_Users_UserId",
                table: "ScLotteryBookingRequests"
            );

            // Drop new PKs
            migrationBuilder.DropPrimaryKey(
                name: "PK_ScLotteryBookingRequests",
                table: "ScLotteryBookingRequests"
            );
            migrationBuilder.DropPrimaryKey(
                name: "PK_ScLotteryDateSelections",
                table: "ScLotteryDateSelections"
            );

            // Rename tables back
            migrationBuilder.RenameTable(
                name: "ScLotteryBookingRequests",
                newName: "ScTrialBookingRequests"
            );
            migrationBuilder.RenameTable(
                name: "ScLotteryDateSelections",
                newName: "ScTrialDateSelections"
            );

            // Rename indexes back for ScLotteryBookingRequests -> ScTrialBookingRequests
            migrationBuilder.RenameIndex(
                name: "IX_ScLotteryBookingRequests_LotteryId",
                table: "ScTrialBookingRequests",
                newName: "IX_ScTrialBookingRequests_LotteryId"
            );
            migrationBuilder.RenameIndex(
                name: "IX_ScLotteryBookingRequests_UserId",
                table: "ScTrialBookingRequests",
                newName: "IX_ScTrialBookingRequests_UserId"
            );
            migrationBuilder.RenameIndex(
                name: "IX_CaseNumber_Ascending",
                table: "ScTrialBookingRequests",
                newName: "IX_CaseNumber_Ascending"
            );
            migrationBuilder.RenameIndex(
                name: "IX_LotteryStartDate_Ascending",
                table: "ScTrialBookingRequests",
                newName: "IX_LotteryStartDate_Ascending"
            );
            migrationBuilder.RenameIndex(
                name: "IX_ProcessingTimestamp_Ascending",
                table: "ScTrialBookingRequests",
                newName: "IX_ProcessingTimestamp_Ascending"
            );
            migrationBuilder.RenameIndex(
                name: "IX_ProcessingTimestamp_Email_RequestedByName_Ascending",
                table: "ScTrialBookingRequests",
                newName: "IX_ProcessingTimestamp_Email_RequestedByName_Ascending"
            );
            migrationBuilder.RenameIndex(
                name: "IX_TaskRunner_Lottery",
                table: "ScTrialBookingRequests",
                newName: "IX_TaskRunner_Lottery"
            );

            // Rename indexes back for ScLotteryDateSelections -> ScTrialDateSelections
            migrationBuilder.RenameIndex(
                name: "IX_ScLotteryDateSelections_BookingRequestId",
                table: "ScTrialDateSelections",
                newName: "IX_ScTrialDateSelections_BookingRequestId"
            );

            // Add old PKs back
            migrationBuilder.AddPrimaryKey(
                name: "PK_ScTrialBookingRequests",
                table: "ScTrialBookingRequests",
                column: "Id"
            );
            migrationBuilder.AddPrimaryKey(
                name: "PK_ScTrialDateSelections",
                table: "ScTrialDateSelections",
                column: "Id"
            );

            // Add foreign keys back with old names
            migrationBuilder.AddForeignKey(
                name: "FK_ScTrialDateSelections_ScTrialBookingRequests_BookingRequest~",
                table: "ScTrialDateSelections",
                column: "BookingRequestId",
                principalTable: "ScTrialBookingRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
            migrationBuilder.AddForeignKey(
                name: "FK_ScTrialBookingRequests_ScLotteries_LotteryId",
                table: "ScTrialBookingRequests",
                column: "LotteryId",
                principalTable: "ScLotteries",
                principalColumn: "Id"
            );
            migrationBuilder.AddForeignKey(
                name: "FK_ScTrialBookingRequests_Users_UserId",
                table: "ScTrialBookingRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id"
            );
        }
    }
}
