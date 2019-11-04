using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HandBookApi.Migrations.HandBookSqlServer
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Base_Books",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ReMark = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Game_Settings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    PassWord = table.Column<string>(nullable: true),
                    ReMark = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    DeadLine = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game_Settings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Base_Books");

            migrationBuilder.DropTable(
                name: "Game_Settings");
        }
    }
}
