using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eximia.CsharpCourse.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class inclusao_coluna_paymentMethodWasRefund_tabela_orders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PaymentMethodWasRefunded",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethodWasRefunded",
                table: "Orders");
        }
    }
}
