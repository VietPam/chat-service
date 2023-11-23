using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chat_service_se357.Migrations
{
    public partial class _103 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_conversation_tb_message_messagesID",
                table: "tb_conversation");

            migrationBuilder.DropIndex(
                name: "IX_tb_conversation_messagesID",
                table: "tb_conversation");

            migrationBuilder.DropColumn(
                name: "messagesID",
                table: "tb_conversation");

            migrationBuilder.AddColumn<long>(
                name: "SqlConversationID",
                table: "tb_message",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_message_SqlConversationID",
                table: "tb_message",
                column: "SqlConversationID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_message_tb_conversation_SqlConversationID",
                table: "tb_message",
                column: "SqlConversationID",
                principalTable: "tb_conversation",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_message_tb_conversation_SqlConversationID",
                table: "tb_message");

            migrationBuilder.DropIndex(
                name: "IX_tb_message_SqlConversationID",
                table: "tb_message");

            migrationBuilder.DropColumn(
                name: "SqlConversationID",
                table: "tb_message");

            migrationBuilder.AddColumn<long>(
                name: "messagesID",
                table: "tb_conversation",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_tb_conversation_messagesID",
                table: "tb_conversation",
                column: "messagesID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_conversation_tb_message_messagesID",
                table: "tb_conversation",
                column: "messagesID",
                principalTable: "tb_message",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
