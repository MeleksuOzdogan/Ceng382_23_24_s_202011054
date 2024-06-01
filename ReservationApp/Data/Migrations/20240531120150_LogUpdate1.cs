using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class LogUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Reservations_reservationID",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_reservationID",
                table: "Logs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Logs_reservationID",
                table: "Logs",
                column: "reservationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Reservations_reservationID",
                table: "Logs",
                column: "reservationID",
                principalTable: "Reservations",
                principalColumn: "ReservationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
