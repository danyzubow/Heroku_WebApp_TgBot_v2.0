using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PorterOfChat.Control;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace WebApp_tg_bot2.TgBotCat.Control.User_Cmd_OnMessage
{
    public class loser : Command
    {
        public override string NameCommand { get; } = "/pidor";

        protected override async void Execution(Message m)
        {
            if (Data.GetChat(m) == null)
            {
                await Tclient.SendTextMessageAsync(ChatID(m), "Список пустий🐷");
                return;
            }

            var random = new Random();
            if (ThisChat.LockGroupPidor) return;
            if (ThisChat.users.Count == 0)
            {
                await Tclient.SendTextMessageAsync(chatID, "Список пустий🐷");
                return;
            }

            var refreshPidor = ThisChat.DatePidor == null;

            if (!refreshPidor)
                refreshPidor = ThisChat.DatePidor !=
                               DateTime.Now.AddHours(3).ToString("d");
            if (refreshPidor)

            {
                var rand = random.Next(0, ThisChat.users.Count);
                thisUser = FindUserFromDic(m, rand);

                var PidorNew = setPidor(ThisChat, thisUser);
                ThisChat.LockGroupPidor = true;
                new Task(() => FindingPidor(ThisChat, PidorNew)).Start();
            }
            else
            {
                await Tclient.SendTextMessageAsync(chatID,
                    $"⚠️<i>Сьогодні на вахті</i>⚠️\n <b>{ThisChat.Pidor}</b>", ParseMode.Html);
                await Tclient.SendTextMessageAsync(chatID, ThisChat.FullPidor + " ⬅️ Link");
            }


        }
    }
}
