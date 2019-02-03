using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PorterOfChat.Control;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace WebApp_tg_bot2.TgBotCat.Control.User_Cmd_OnMessage
{
    public class batya : Command
    {
        public override string NameCommand { get; } = "/batya";
        protected override  void Execution(Message m)
        {

            if (Data.GetChat(m) == null)
            {
                 SendTextMessageAsync(chatID, "Список пустий🐷");
                return;
            }

            var random = new Random();
            if (ThisChat.LockGroupDad) return;
            if (ThisChat.users.Count == 0)
            {
                SendTextMessageAsync(chatID, "Список пустий🐷");
                return;
            }

            var refreshPidor = ThisChat.DateDad == null;

            if (!refreshPidor)
                refreshPidor = ThisChat.DateDad !=
                               DateTime.Now.AddHours(3).ToString("d");
            if (refreshPidor)

            {
                var rand = random.Next(0, ThisChat.users.Count);
                thisUser = FindUserFromDic(ChatIDs(m), rand);

                var DadToday = setBatya(ThisChat, thisUser);
                ThisChat.LockGroupDad = true;
                new Task(() => FindingDad(chatID, ThisChat, DadToday)).Start();
            }
            else
            {
                SendTextMessageAsync(chatID, $"⚠️<b>Сьогодні {ThisChat.Dad}</b>",
                    ParseMode.Html);
                SendTextMessageAsync(chatID, ThisChat.FullDad + " ⬅️ Link");
            }
        }
    }
}
