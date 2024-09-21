using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eximia.CsharpCourse.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class ajuste_payments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gateway",
                table: "Payments",
                newName: "Method");

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Payments",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Installments",
                table: "Payments",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Installments",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "Method",
                table: "Payments",
                newName: "Gateway");
        }
    }
}
