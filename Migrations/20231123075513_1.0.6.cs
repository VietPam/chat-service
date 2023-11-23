using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chat_service_se357.Migrations
{
    public partial class _106 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SqlConversationSqlUser");

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
        }
    }
}
