using Microsoft.EntityFrameworkCore.Migrations;

namespace ExemploEF.Migrations
{
    public partial class TabelaAluno_Disciplina : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aluno_Disciplina",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunoRA = table.Column<int>(nullable: true),
                    DisciplinaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno_Disciplina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aluno_Disciplina_Aluno_AlunoRA",
                        column: x => x.AlunoRA,
                        principalTable: "Aluno",
                        principalColumn: "RA",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Aluno_Disciplina_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_Disciplina_AlunoRA",
                table: "Aluno_Disciplina",
                column: "AlunoRA");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_Disciplina_DisciplinaId",
                table: "Aluno_Disciplina",
                column: "DisciplinaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aluno_Disciplina");
        }
    }
}
