using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarvelAPI.Data.Migrations
{
    public partial class UpdateMovieAndTvEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReleaseYear",
                table: "TVShows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReleaseYear",
                table: "Movies",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
