using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chat_service_se357.Migrations
{
    public partial class _102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_message_tb_user_senderID",
                table: "tb_message");

            migrationBuilder.DropIndex(
                name: "IX_tb_message_senderID",
                table: "tb_message");

            migrationBuilder.DropColumn(
                name: "senderID",
                table: "tb_message");

            migrationBuilder.DropColumn(
                name: "time",
                table: "tb_message");

            migrationBuilder.AddColumn<bool>(
                name: "is_shop",
                table: "tb_user",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "clientCode",
                table: "tb_message",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "shopCode",
                table: "tb_message",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_shop",
                table: "tb_user");

            migrationBuilder.DropColumn(
                name: "clientCode",
                table: "tb_message");

            migrationBuilder.DropColumn(
                name: "shopCode",
                table: "tb_message");

            migrationBuilder.AddColumn<long>(
                name: "senderID",
                table: "tb_message",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "time",
                table: "tb_message",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_tb_message_senderID",
                table: "tb_message",
                column: "senderID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_message_tb_user_senderID",
                table: "tb_message",
                column: "senderID",
                principalTable: "tb_user",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
