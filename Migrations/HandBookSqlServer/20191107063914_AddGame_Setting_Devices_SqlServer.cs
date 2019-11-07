using Microsoft.EntityFrameworkCore.Migrations;

namespace HandBookApi.Migrations.HandBookSqlServer
{
    public partial class AddGame_Setting_Devices_SqlServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Devices",
                table: "Game_Settings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Devices",
                table: "Game_Settings");
        }
    }
}
