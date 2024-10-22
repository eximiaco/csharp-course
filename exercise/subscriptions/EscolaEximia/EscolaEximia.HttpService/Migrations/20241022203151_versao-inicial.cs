using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscolaEximia.HttpService.Migrations
{
    /// <inheritdoc />
    public partial class versaoinicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Sexo = table.Column<string>(type: "varchar(20)", nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Cpf);
                });

            migrationBuilder.CreateTable(
                name: "Inscricoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlunoCpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Responsavel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ativa = table.Column<bool>(type: "bit", nullable: false),
                    TurmaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscricoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vagas = table.Column<int>(type: "int", nullable: false),
                    Masculino = table.Column<bool>(type: "bit", nullable: false),
                    Feminino = table.Column<bool>(type: "bit", nullable: false),
                    LimiteIdade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Inscricoes");

            migrationBuilder.DropTable(
                name: "Turmas");
        }
    }
}
