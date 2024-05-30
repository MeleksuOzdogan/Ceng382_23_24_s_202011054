using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReservationTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReserverName",
                table: "Reservations",
                newName: "UpdatedBy");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReserverId",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReserverId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Reservations",
                newName: "ReserverName");
        }
    }
}
