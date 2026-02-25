using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GemManagment.DAL.Migrations
{
    /// <inheritdoc />
    public partial class editpatternforemail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "EmailCheckFormat1",
                table: "Trainer");

            migrationBuilder.DropCheckConstraint(
                name: "EmailCheckFormat",
                table: "Member");

            migrationBuilder.AddCheckConstraint(
                name: "EmailCheckFormat1",
                table: "Trainer",
                sql: "Email like '%@%._%'");

            migrationBuilder.AddCheckConstraint(
                name: "EmailCheckFormat",
                table: "Member",
                sql: "Email like '%@%._%'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "EmailCheckFormat1",
                table: "Trainer");

            migrationBuilder.DropCheckConstraint(
                name: "EmailCheckFormat",
                table: "Member");

            migrationBuilder.AddCheckConstraint(
                name: "EmailCheckFormat1",
                table: "Trainer",
                sql: "Email like '_%@_$._%'");

            migrationBuilder.AddCheckConstraint(
                name: "EmailCheckFormat",
                table: "Member",
                sql: "Email like '_%@_$._%'");
        }
    }
}
