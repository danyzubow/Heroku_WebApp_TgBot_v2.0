using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp_tg_bot2.Migrations
{
    public partial class mess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "MessagesLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    chatId = table.Column<int>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessagesLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessagesLogs_chats_chatId",
                        column: x => x.chatId,
                        principalTable: "chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

          

            migrationBuilder.CreateIndex(
                name: "IX_MessagesLogs_chatId",
                table: "MessagesLogs",
                column: "chatId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_users_cChatId",
            //    table: "users",
            //    column: "cChatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessagesLogs");

          
        }
    }
}
