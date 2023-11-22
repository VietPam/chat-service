using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace chat_service_se357.Migrations
{
    public partial class _100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_conversation",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clientCode = table.Column<string>(type: "text", nullable: false),
                    shopCode = table.Column<string>(type: "text", nullable: false),
                    messagesID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_conversation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tb_user",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    conversationsID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_user", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_user_tb_conversation_conversationsID",
                        column: x => x.conversationsID,
                        principalTable: "tb_conversation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_message",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    senderID = table.Column<long>(type: "bigint", nullable: false),
                    time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    message = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_message", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_message_tb_user_senderID",
                        column: x => x.senderID,
                        principalTable: "tb_user",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_conversation_messagesID",
                table: "tb_conversation",
                column: "messagesID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_message_senderID",
                table: "tb_message",
                column: "senderID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_user_conversationsID",
                table: "tb_user",
                column: "conversationsID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_conversation_tb_message_messagesID",
                table: "tb_conversation",
                column: "messagesID",
                principalTable: "tb_message",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_conversation_tb_message_messagesID",
                table: "tb_conversation");

            migrationBuilder.DropTable(
                name: "tb_message");

            migrationBuilder.DropTable(
                name: "tb_user");

            migrationBuilder.DropTable(
                name: "tb_conversation");
        }
    }
}
