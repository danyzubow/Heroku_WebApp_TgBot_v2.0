using PorterOfChat.Bot;
using PorterOfChat.Control.Admin_Cmd_OnCallBackQuery;
using PorterOfChat.Control.AdminComands_onMessage;
using PorterOfChat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using WebApp_tg_bot2.TgBotCat.Control.User_Cmd_OnMessage;
using User = PorterOfChat.Control.Admin_Cmd_OnCallBackQuery.User;

namespace PorterOfChat.Control
{


    public class TransporterCMD
    {
        List<Command> Comands_onMessage;
        List<Command> ComandsAdmin_onMessage;
        List<Command> ComandsAdmin_OnCallbackQuery;

        /// <summary Рреєстрація команд
        /// 
        /// </summary>
        public TransporterCMD()
        {
            Comands_onMessage = new List<Command>()
            {
                new loser(),
                new batya(),
                new leave(),
                new reg(),
                new setfemale(),
                new sheriff(),
                new setmale(),
                new stats(),
            };
            ComandsAdmin_onMessage = new List<Command>()
            {
                new adm(),
                new h(),
                new data(),
                new ping()

            };
            ComandsAdmin_OnCallbackQuery = new List<Command>()
            {
                new Chats(),
                new Users(),
                new Admin_Cmd_OnCallBackQuery.Chat(),
                new Setbat(),
                new SetP(),
                new User()
            };
        }


        public void OnMessage(TelegramBotClient sender, Message m)
        {
            
            if(m.Text==null) return;
            if (!m.Text.Contains(Settings.NameBot) && (m.Chat.Id != Settings.AdminChatId)) return;
           

            string comd = m.Text.Split(Settings.NameBot).First(); ;

            Command cmd =
                (m.Chat.Id == Settings.AdminChatId ? ComandsAdmin_onMessage : Comands_onMessage).Find(t =>
                    t.NameCommand == comd);
            if (cmd == null)
            {
                new InfoService($"Команда \'{comd}\' не найдена");
            }
            else
            {
                cmd.Exec(sender, m);
            }
        }
        public void OnCallbackQuery(object sender, CallbackQuery c)
        {

            if (c.From.Id != Settings.AdminChatId) return;
            string comd = c.Data.Split("_").First();
            Command cmd =
                (c.From.Id == Settings.AdminChatId ? ComandsAdmin_OnCallbackQuery : null).Find(t =>
                   t.NameCommand == comd);
            if (cmd == null)
            {
                new InfoService($"Команда \'{comd}\' не найдена");
            }
            else
            {
                cmd.Exec(sender, c);
            }
        }
    }

}