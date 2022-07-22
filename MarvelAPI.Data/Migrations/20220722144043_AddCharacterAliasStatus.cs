using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarvelAPI.Data.Migrations
{
    public partial class AddCharacterAliasStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Aliases",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aliases",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Characters");
        }
    }
}
