using PorterOfChat.Bot.Model;
using Microsoft.EntityFrameworkCore;

namespace WebApp_tg_bot2.TgBotCat.Model.Chat
{
    public class ChatDB : DbContext
    {

        public DbSet<cChat> chats;
      

        public ChatDB()
        {
            Database.EnsureCreated();

        }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=TestTelgram.mssql.somee.com;Database=TestTelgram;user=seadogs4_SQLLogin_1;password=cfcqfp1xdp;");
        }
    }
}
