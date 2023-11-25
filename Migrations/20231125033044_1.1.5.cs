using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chat_service_se357.Migrations
{
    public partial class _115 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ID_code",
                table: "tb_user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ID_code",
                table: "tb_message",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ID_code",
                table: "tb_conversation",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID_code",
                table: "tb_user");

            migrationBuilder.DropColumn(
                name: "ID_code",
                table: "tb_message");

            migrationBuilder.DropColumn(
                name: "ID_code",
                table: "tb_conversation");
        }
    }
}
