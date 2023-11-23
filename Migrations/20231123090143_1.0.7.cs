using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chat_service_se357.Migrations
{
    public partial class _107 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_conversation_tb_user_SqlUserID",
                table: "tb_conversation");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_message_tb_conversation_SqlConversationID",
                table: "tb_message");

            migrationBuilder.DropIndex(
                name: "IX_tb_message_SqlConversationID",
                table: "tb_message");

            migrationBuilder.DropIndex(
                name: "IX_tb_conversation_SqlUserID",
                table: "tb_conversation");

            migrationBuilder.DropColumn(
                name: "SqlConversationID",
                table: "tb_message");

            migrationBuilder.DropColumn(
                name: "SqlUserID",
                table: "tb_conversation");

            migrationBuilder.AddColumn<long>(
                name: "conversationID",
                table: "tb_message",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "userID",
                table: "tb_conversation",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_tb_message_conversationID",
                table: "tb_message",
                column: "conversationID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_tb_message_tb_conversation_conversationID",
                table: "tb_message",
                column: "conversationID",
                principalTable: "tb_conversation",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_conversation_tb_user_userID",
                table: "tb_conversation");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_message_tb_conversation_conversationID",
                table: "tb_message");

            migrationBuilder.DropIndex(
                name: "IX_tb_message_conversationID",
                table: "tb_message");

            migrationBuilder.DropIndex(
                name: "IX_tb_conversation_userID",
                table: "tb_conversation");

            migrationBuilder.DropColumn(
                name: "conversationID",
                table: "tb_message");

            migrationBuilder.DropColumn(
                name: "userID",
                table: "tb_conversation");

            migrationBuilder.AddColumn<long>(
                name: "SqlConversationID",
                table: "tb_message",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SqlUserID",
                table: "tb_conversation",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_message_SqlConversationID",
                table: "tb_message",
                column: "SqlConversationID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_tb_message_tb_conversation_SqlConversationID",
                table: "tb_message",
                column: "SqlConversationID",
                principalTable: "tb_conversation",
                principalColumn: "ID");
        }
    }
}
