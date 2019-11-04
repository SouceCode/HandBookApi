using Microsoft.EntityFrameworkCore.Migrations;

namespace HandBookApi.Migrations
{
    public partial class AddGame_Setting_TryType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TryType",
                table: "Game_Settings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TryType",
                table: "Game_Settings");
        }
    }
}
