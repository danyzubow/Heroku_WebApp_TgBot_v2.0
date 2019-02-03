using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp_tg_bot2.Migrations
{
    public partial class addDatetoMessageLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "MessagesLogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "MessagesLogs");
        }
    }
}
