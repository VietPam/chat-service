using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chat_service_se357.Migrations
{
    public partial class _118 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_change",
                table: "tb_conversation");

            migrationBuilder.AddColumn<string>(
                name: "IdHub",
                table: "tb_user",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdHub",
                table: "tb_user");

            migrationBuilder.AddColumn<long>(
                name: "last_change",
                table: "tb_conversation",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
