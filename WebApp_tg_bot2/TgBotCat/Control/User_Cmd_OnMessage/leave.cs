using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PorterOfChat.Control;
using Telegram.Bot.Types;

namespace WebApp_tg_bot2.TgBotCat.Control.User_Cmd_OnMessage
{
    public class leave : Command
    {
        public override string NameCommand { get; } = "/leave";
        protected override  void Execution(Message m)
        {

            if (Data.GetChat(m) == null)
            {
                SendTextMessageAsync(chatID, "список пустий😕");
            }
            else
            {
                var sName = m.From.FirstName + " " + m.From.LastName;
                if (ContainsUserFromDic(m))
                {
                    thisUser = Data.GetChat(m).users.Find(t =>
                        t.Id_tg == UserIDs(m));
                    Data.GetChat(m).users.Remove(thisUser);
                   SendTextMessageAsync(chatID, sName + " ліває🚮");
                }
                else
                {
                    SendTextMessageAsync(chatID, sName + " ти не в темі🤔");
                }
            }


        }
    }
}
