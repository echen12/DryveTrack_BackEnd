using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DryveTrackBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class RemovedFKFromVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Insurance",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "Odometer",
                table: "Vehicle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Insurance",
                table: "Vehicle",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Odometer",
                table: "Vehicle",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
