using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace tcc_core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Projeto_ProjetoId",
                table: "Agendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Usuario_UsuarioId",
                table: "Agendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimentacao_Projeto_ProjetoId",
                table: "Movimentacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimentacao_Usuario_UsuarioId",
                table: "Movimentacao");

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Projeto",
                type: "longtext",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "ProjetoId",
                table: "Movimentacao",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProjetoId",
                table: "Agendamento",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Observacoes",
                table: "Agendamento",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Feedback",
                table: "Agendamento",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.CreateTable(
                name: "UserTestes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Senha = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTestes", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Projeto_ProjetoId",
                table: "Agendamento",
                column: "ProjetoId",
                principalTable: "Projeto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Usuario_UsuarioId",
                table: "Agendamento",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movimentacao_Projeto_ProjetoId",
                table: "Movimentacao",
                column: "ProjetoId",
                principalTable: "Projeto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movimentacao_Usuario_UsuarioId",
                table: "Movimentacao",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Projeto_ProjetoId",
                table: "Agendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Usuario_UsuarioId",
                table: "Agendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimentacao_Projeto_ProjetoId",
                table: "Movimentacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimentacao_Usuario_UsuarioId",
                table: "Movimentacao");

            migrationBuilder.DropTable(
                name: "UserTestes");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Projeto");

            migrationBuilder.AlterColumn<int>(
                name: "ProjetoId",
                table: "Movimentacao",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjetoId",
                table: "Agendamento",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observacoes",
                table: "Agendamento",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Feedback",
                table: "Agendamento",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Projeto_ProjetoId",
                table: "Agendamento",
                column: "ProjetoId",
                principalTable: "Projeto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Usuario_UsuarioId",
                table: "Agendamento",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movimentacao_Projeto_ProjetoId",
                table: "Movimentacao",
                column: "ProjetoId",
                principalTable: "Projeto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movimentacao_Usuario_UsuarioId",
                table: "Movimentacao",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
