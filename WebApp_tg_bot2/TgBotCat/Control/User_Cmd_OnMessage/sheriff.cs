using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PorterOfChat.Bot.Model;
using PorterOfChat.Control;
using Telegram.Bot.Types;

namespace WebApp_tg_bot2.TgBotCat.Control.User_Cmd_OnMessage
{
    public class sheriff : Command
    {
        public override string NameCommand { get; } = "/sheriff";
        protected override  void Execution(Message m)
        {

            if (Data.GetChat(m) == null)
            {
               SendTextMessageAsync(chatID, "Список пустий🐷");
                return;
            }

            var tmpGroup2 = Data.GetChat(m);
            if (tmpGroup2.users.Count == 0)
            {
                SendTextMessageAsync(chatID, "Список пустий🐷");
                return;
            }

            var stats2 = "Головний по 🌈вахті";
            var users = new List<cUser>();
            var max = 0;
            foreach (var t in tmpGroup2.users)
            {
                if (t.CountPidor == max)
                {
                    users.Add(t);
                    max = t.CountPidor;
                }

                if (t.CountPidor > max)
                {
                    users.Clear();
                    users.Add(t);
                    max = t.CountPidor;
                }
            }


            foreach (var user in users)
                stats2 += $"\n🥇" + user.FullName + " - " + user.CountPidor +
                          " раз(а)";


            SendTextMessageAsync(chatID, stats2);

        }
    }
}
