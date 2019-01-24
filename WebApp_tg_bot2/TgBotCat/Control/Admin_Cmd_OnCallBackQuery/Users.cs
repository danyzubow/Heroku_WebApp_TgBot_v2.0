using System.Collections.Generic;
using PorterOfChat.Bot.Model;
using PorterOfChat.Control;
using PorterOfChat.Model;
using Telegram.Bot.Types;


namespace PorterOfChat.Control.Admin_Cmd_OnCallBackQuery
{
    public class Users : Command
    {
        public override string NameCommand { get; } = "Users";

        protected override void Execution(CallbackQuery c)
        {
            List<Button> btns=new List<Button>();
            User usr=new User();
            foreach (var t in ThisChat.users)
            {
                btns.Add(usr.getButton(ThisChat,t));
            }

            new Menu(
                new Chat().getButton(Argument[0]),
                btns.ToArray(),
                c.Message.MessageId,
                $"Група <b>'{ThisChat.Name}</b>'" +
                $"[{ThisChat.Id_tg}] - " +
                $"\n'Пдр' =<b>{ThisChat.FullPidor}</b>; [Последний раз(дата):{ThisChat.DatePidor}]" +
                $"\n'Батя' =<b>{ThisChat.FullDad}</b>; [Последний раз(дата):{ThisChat.DateDad}]" +
                $"\nКоличество учасников={ThisChat.users.Count}" +
                $"\nВсего учасников в чате={ThisChat.CountMembers}"
                + $"\nУчасники:");
          
        }
    }
}