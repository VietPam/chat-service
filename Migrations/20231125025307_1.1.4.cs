using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chat_service_se357.Migrations
{
    public partial class _114 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_conversation_tb_user_sqlUserID",
                table: "tb_conversation");

            migrationBuilder.DropIndex(
                name: "IX_tb_conversation_sqlUserID",
                table: "tb_conversation");

            migrationBuilder.DropColumn(
                name: "sqlUserID",
                table: "tb_conversation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "sqlUserID",
                table: "tb_conversation",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_tb_conversation_sqlUserID",
                table: "tb_conversation",
                column: "sqlUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_conversation_tb_user_sqlUserID",
                table: "tb_conversation",
                column: "sqlUserID",
                principalTable: "tb_user",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
