using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PorterOfChat.Control;
using PorterOfChat.Bot.Model;
using PorterOfChat.Service;
using Telegram.Bot.Types;


namespace PorterOfChat.Control.Admin_Cmd_OnCallBackQuery
{
    class Setbat : Command
    {
        public override string NameCommand { get; } = "Setbat";

        protected override void Execution(CallbackQuery c)
        {
            int count = 1;
          
            if (ThisChat.FullDad == ""|| ThisChat.FullDad==null)
            {
                new InfoService($"В чате '{ThisChat.Name}' еще нет 'бати' ");
                return;
            }

            cUser c_current_user = ThisChat.users.Find(t =>
                t.FullName == ThisChat.FullDad);
            c_current_user.CountDad = ((int.Parse(c_current_user.CountDad)) - 1).ToString();
  
            setBatya(ThisChat, thisUser);
            string outStr =
                $"<b>Complete.</b> Чат=> <b>* ) '{ThisChat.Name}'</b> [{ThisChat.Id_tg}] - " +
                $"\n'Підор' =<b>{ThisChat.FullPidor}</b>; [Последний раз(дата):{ThisChat.DatePidor}]" +
                $"\n'Батя' =<b>{ThisChat.FullDad}</b>; [Последний раз(дата):{ThisChat.DateDad}]";
            new Menu
            (
                new User().getButton_Name("Ок", Argument[0], Argument[1]),
                c.Message.MessageId,
                outStr
            );
          
        }
    }
}