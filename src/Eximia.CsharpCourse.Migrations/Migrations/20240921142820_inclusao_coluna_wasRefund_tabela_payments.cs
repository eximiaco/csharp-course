using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eximia.CsharpCourse.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class inclusao_coluna_wasRefund_tabela_payments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WasRefund",
                table: "Payments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WasRefund",
                table: "Payments");
        }
    }
}
