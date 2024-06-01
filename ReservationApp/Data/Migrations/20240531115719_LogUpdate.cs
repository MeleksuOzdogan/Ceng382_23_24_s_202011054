using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class LogUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    operationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    reservationID = table.Column<int>(type: "int", nullable: false),
                    operation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.id);
                    table.ForeignKey(
                        name: "FK_Logs_Reservations_reservationID",
                        column: x => x.reservationID,
                        principalTable: "Reservations",
                        principalColumn: "ReservationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_reservationID",
                table: "Logs",
                column: "reservationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
