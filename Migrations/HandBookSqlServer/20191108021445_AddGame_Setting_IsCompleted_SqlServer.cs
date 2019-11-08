using Microsoft.EntityFrameworkCore.Migrations;

namespace HandBookApi.Migrations.HandBookSqlServer
{
    public partial class AddGame_Setting_IsCompleted_SqlServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Game_Settings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Game_Settings");
        }
    }
}
