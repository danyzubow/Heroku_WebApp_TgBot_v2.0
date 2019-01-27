using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PorterOfChat.Control;
using Telegram.Bot.Types;

namespace WebApp_tg_bot2.TgBotCat.Control.User_Cmd_OnMessage
{
    public class setmale : Command
    {
        public override string NameCommand { get; } = "/setmale";
        protected override async void Execution(Message m)
        {
            if (!ContainsUserFromDic(m))
            {
                await Tclient.SendTextMessageAsync(chatID,
                    m.From.FirstName + " ти не в темі🤔");
                return;
            }

            thisUser = Data.GetChat(m).users.Find(t =>
                t.Id_tg == UserIDs(m));
            thisUser.GenderFemale = false;
            await Tclient.SendTextMessageAsync(chatID, thisUser.Name + " стає пациком💪🏻");
        }
    }
}
