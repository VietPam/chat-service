using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chat_service_se357.Migrations
{
    public partial class _101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_user_tb_conversation_conversationsID",
                table: "tb_user");

            migrationBuilder.DropIndex(
                name: "IX_tb_user_conversationsID",
                table: "tb_user");

            migrationBuilder.DropColumn(
                name: "conversationsID",
                table: "tb_user");

            migrationBuilder.AddColumn<long>(
                name: "SqlUserID",
                table: "tb_conversation",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_conversation_SqlUserID",
                table: "tb_conversation",
                column: "SqlUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_conversation_tb_user_SqlUserID",
                table: "tb_conversation",
                column: "SqlUserID",
                principalTable: "tb_user",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_conversation_tb_user_SqlUserID",
                table: "tb_conversation");

            migrationBuilder.DropIndex(
                name: "IX_tb_conversation_SqlUserID",
                table: "tb_conversation");

            migrationBuilder.DropColumn(
                name: "SqlUserID",
                table: "tb_conversation");

            migrationBuilder.AddColumn<long>(
                name: "conversationsID",
                table: "tb_user",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_tb_user_conversationsID",
                table: "tb_user",
                column: "conversationsID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_user_tb_conversation_conversationsID",
                table: "tb_user",
                column: "conversationsID",
                principalTable: "tb_conversation",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
