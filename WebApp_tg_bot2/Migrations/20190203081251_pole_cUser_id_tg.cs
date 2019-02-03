using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp_tg_bot2.Migrations
{
    public partial class pole_cUser_id_tg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_tg",
                table: "MessagesLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "MessagesLogs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessagesLogs_userId",
                table: "MessagesLogs",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessagesLogs_users_userId",
                table: "MessagesLogs",
                column: "userId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessagesLogs_users_userId",
                table: "MessagesLogs");

            migrationBuilder.DropIndex(
                name: "IX_MessagesLogs_userId",
                table: "MessagesLogs");

            migrationBuilder.DropColumn(
                name: "id_tg",
                table: "MessagesLogs");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "MessagesLogs");
        }
    }
}
