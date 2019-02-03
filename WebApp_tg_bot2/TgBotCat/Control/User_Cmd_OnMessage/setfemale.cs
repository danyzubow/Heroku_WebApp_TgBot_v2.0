using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PorterOfChat.Control;
using Telegram.Bot.Types;

namespace WebApp_tg_bot2.TgBotCat.Control.User_Cmd_OnMessage
{
    public class setfemale : Command
    {
        public override string NameCommand { get; } = "/setfemale";
        protected override  void Execution(Message m)
        {
            if (!ContainsUserFromDic(m))
            {
                SendTextMessageAsync(chatID,
                    m.From.FirstName + " ти не в темі🤔");
                return;
            }

            thisUser = Data.GetChat(m).users.Find(t =>
                t.Id_tg == UserIDs(m));
            thisUser.GenderFemale = true;
           SendTextMessageAsync(chatID, thisUser.Name + " стає подругой👠");
        }
    }
}
