using Microsoft.EntityFrameworkCore.Migrations;

namespace HandBookApi.Migrations.HandBookSqlServer
{
    public partial class AddUsersId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "Game_Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "Base_Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Game_Settings");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Base_Books");
        }
    }
}
