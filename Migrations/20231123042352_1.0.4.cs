using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chat_service_se357.Migrations
{
    public partial class _104 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "shopCode",
                table: "tb_message",
                newName: "senderCode");

            migrationBuilder.RenameColumn(
                name: "clientCode",
                table: "tb_message",
                newName: "receiverCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "senderCode",
                table: "tb_message",
                newName: "shopCode");

            migrationBuilder.RenameColumn(
                name: "receiverCode",
                table: "tb_message",
                newName: "clientCode");
        }
    }
}
