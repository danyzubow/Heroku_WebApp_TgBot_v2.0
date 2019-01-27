using PorterOfChat.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PorterOfChat.Bot.Model;
using Telegram.Bot.Types;

namespace WebApp_tg_bot2.TgBotCat.Control.User_Cmd_OnMessage
{
    public class reg : Command
    {
        public override string NameCommand { get; } = "/reg";
        protected override async void Execution(Message m)
        {
            if (ThisChat == null) //(!ContainsGroupFromDic(e.Message.cChat.Id_tg.ToString()))
            {
                string nameGroup;
                if (m.Chat.Title == null)
                    nameGroup = m.Chat.FirstName + "_" + m.Chat.LastName;
                else
                    nameGroup = m.Chat.Title;
                var newChat = new cChat(new List<cUser>())
                {
                    Id_tg = ChatIDs(m),
                    Name = nameGroup
                };
                Data.AddChat(newChat);
            }

            if (ContainsUserFromDic(ChatIDs(m), m.From.Id))
            {
                await Tclient.SendTextMessageAsync(m.Chat.Id, "Ти мило вже впустив❤️");
            }
            else
            {
                var username = m.From.Username;
                var sName = m.From.FirstName + " " + m.From.LastName +
                            (username != null ? $" (@{username})" : "");
                var newUser = new cUser
                {
                    Id_tg = m.From.Id,
                    FullName = sName,
                    Name = m.From.FirstName,
                    CountPidor = 0,
                    CountDad = 0
                };
                Data.GetChat(m).users.Add(newUser);

                await Tclient.SendTextMessageAsync(chatID,
                    sName + " кидає мило на підлогу🏋️\n /setfemale - Стати подругой👠" +
                    "\n /setmale - Стати пациком💪🏻");
            }
        }
    }
}
