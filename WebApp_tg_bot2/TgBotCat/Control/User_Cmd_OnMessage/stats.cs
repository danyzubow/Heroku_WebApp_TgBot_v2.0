using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PorterOfChat.Control;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace WebApp_tg_bot2.TgBotCat.Control.User_Cmd_OnMessage
{
    public class stats : Command
    {
        public override string NameCommand { get; } = "/stats";
        protected override async void Execution(Message m)
        {

            if (Data.GetChat(m) == null)
            {
                await Tclient.SendTextMessageAsync(chatID, "Список пустий🐷");
                return;
            }

            if (ThisChat.users.Count == 0)
            {
                await Tclient.SendTextMessageAsync(chatID, "Список пустий🐷");
                return;
            }

            var stats = "<b>Результати 🌈ВАХТЕРИ Дня</b>";
            var count = 1;
            foreach (var t in ThisChat.users)
            {
                stats += $"\n<b>{count}</b>)" + t.FullName + " - " + t.CountPidor +
                         " раз(а)";
                count++;
            }

            var statsDat = "\n<b>Результати 🔝БАТЯ дня</b>";
            count = 1;
            foreach (var t in ThisChat.users)
            {
                statsDat += $"\n<b>{count}</b>)" + t.FullName + " - " + t.CountDad +
                            " раз(а)";
                count++;
            }

            await Tclient.SendTextMessageAsync(chatID, stats, ParseMode.Html);
            await Tclient.SendTextMessageAsync(chatID, statsDat, ParseMode.Html);

        }
    }
}
