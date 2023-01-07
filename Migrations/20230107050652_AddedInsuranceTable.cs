using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DryveTrackBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddedInsuranceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Insurance",
                columns: table => new
                {
                    InsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceExpiryDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceProvider = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurance", x => x.InsuranceId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Insurance");
        }
    }
}
