using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tcc_core.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Projeto",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Projeto");
        }
    }
}
