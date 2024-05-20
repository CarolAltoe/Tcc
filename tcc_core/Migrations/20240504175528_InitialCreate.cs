using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace tcc_core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "longtext", nullable: false),
                    Classificacao = table.Column<string>(type: "longtext", nullable: false),
                    QuantidadeAtual = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Projeto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Coordenador = table.Column<string>(type: "longtext", nullable: false),
                    Natureza = table.Column<string>(type: "longtext", nullable: false),
                    DtInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DtFinal = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projeto", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NomeCompleto = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Senha = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Agendamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ResponsavelInterno = table.Column<string>(type: "longtext", nullable: false),
                    ResponsavelExterno = table.Column<string>(type: "longtext", nullable: false),
                    Turma = table.Column<string>(type: "longtext", nullable: false),
                    Descricao = table.Column<string>(type: "longtext", nullable: false),
                    Feedback = table.Column<string>(type: "longtext", nullable: false),
                    Observacoes = table.Column<string>(type: "longtext", nullable: false),
                    DtInicial = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DtFinal = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    QtdPessoas = table.Column<int>(type: "int", nullable: false),
                    ProjetoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agendamento_Projeto_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agendamento_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Movimentacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Responsavel = table.Column<string>(type: "longtext", nullable: false),
                    TipoMovimentacao = table.Column<string>(type: "longtext", nullable: false),
                    DtMovimentacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    ProjetoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimentacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimentacao_Projeto_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimentacao_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MovimentacaoMaterial",
                columns: table => new
                {
                    MovimentacaoId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentacaoMaterial", x => new { x.MovimentacaoId, x.MaterialId });
                    table.ForeignKey(
                        name: "FK_MovimentacaoMaterial_Material_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Material",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimentacaoMaterial_Movimentacao_MovimentacaoId",
                        column: x => x.MovimentacaoId,
                        principalTable: "Movimentacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_ProjetoId",
                table: "Agendamento",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_UsuarioId",
                table: "Agendamento",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacao_ProjetoId",
                table: "Movimentacao",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacao_UsuarioId",
                table: "Movimentacao",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacaoMaterial_MaterialId",
                table: "MovimentacaoMaterial",
                column: "MaterialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agendamento");

            migrationBuilder.DropTable(
                name: "MovimentacaoMaterial");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Movimentacao");

            migrationBuilder.DropTable(
                name: "Projeto");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
