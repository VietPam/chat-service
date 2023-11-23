using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chat_service_se357.Migrations
{
    public partial class _108 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_conversation_tb_user_userID",
                table: "tb_conversation");

            migrationBuilder.DropIndex(
                name: "IX_tb_conversation_userID",
                table: "tb_conversation");

            migrationBuilder.DropColumn(
                name: "userID",
                table: "tb_conversation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "userID",
                table: "tb_conversation",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_tb_conversation_userID",
                table: "tb_conversation",
                column: "userID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_conversation_tb_user_userID",
                table: "tb_conversation",
                column: "userID",
                principalTable: "tb_user",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
