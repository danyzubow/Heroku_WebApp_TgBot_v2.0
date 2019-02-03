using PorterOfChat.Bot.Model;
using Telegram.Bot.Types;

namespace WebApp_tg_bot2.TgBotCat.Model
{
    public class MessageLog
    {
        public int Id { get; set; }
        public cChat chat { get; set; }
        public cUser user { get; set; }
        public int id_tg { get; set; }
        public string FullName { get; set; }
        public string Date { get; set; }
        public string Text { get; set; }

        public MessageLog(cChat chat, Message m)
        {
            this.chat = chat;
            user = chat?.users?.Find(t => t.Id_tg == m.From.Id);
            id_tg = m.From.Id;
            Text = m.Text;
            FullName = m.From.FirstName + " " + m.From.LastName + $" (@{m.From.Username ?? null})";
            Date = m.Date.ToString("G");
        }
        public MessageLog() { }

    }
}
