using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chat_service_se357.Migrations
{
    public partial class _109 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_message_tb_conversation_conversationID",
                table: "tb_message");

            migrationBuilder.RenameColumn(
                name: "conversationID",
                table: "tb_message",
                newName: "conversationsID");

            migrationBuilder.RenameIndex(
                name: "IX_tb_message_conversationID",
                table: "tb_message",
                newName: "IX_tb_message_conversationsID");

            migrationBuilder.CreateTable(
                name: "SqlConversationSqlUser",
                columns: table => new
                {
                    conversationsID = table.Column<long>(type: "bigint", nullable: false),
                    usersID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlConversationSqlUser", x => new { x.conversationsID, x.usersID });
                    table.ForeignKey(
                        name: "FK_SqlConversationSqlUser_tb_conversation_conversationsID",
                        column: x => x.conversationsID,
                        principalTable: "tb_conversation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SqlConversationSqlUser_tb_user_usersID",
                        column: x => x.usersID,
                        principalTable: "tb_user",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SqlConversationSqlUser_usersID",
                table: "SqlConversationSqlUser",
                column: "usersID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_message_tb_conversation_conversationsID",
                table: "tb_message",
                column: "conversationsID",
                principalTable: "tb_conversation",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_message_tb_conversation_conversationsID",
                table: "tb_message");

            migrationBuilder.DropTable(
                name: "SqlConversationSqlUser");

            migrationBuilder.RenameColumn(
                name: "conversationsID",
                table: "tb_message",
                newName: "conversationID");

            migrationBuilder.RenameIndex(
                name: "IX_tb_message_conversationsID",
                table: "tb_message",
                newName: "IX_tb_message_conversationID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_message_tb_conversation_conversationID",
                table: "tb_message",
                column: "conversationID",
                principalTable: "tb_conversation",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
