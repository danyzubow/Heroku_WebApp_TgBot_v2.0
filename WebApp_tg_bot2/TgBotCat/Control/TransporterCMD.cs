using System;
using cat.Bot;
using cat.Service;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace cat.Control
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
                new cat.Control.UserComands_onMessage.loser(),
                new cat.Control.UserComands_onMessage.batya(),
                new cat.Control.UserComands_onMessage.leave(),
                new cat.Control.UserComands_onMessage.reg(),
                new cat.Control.UserComands_onMessage.setfemale(),
                new cat.Control.UserComands_onMessage.sheriff(),
                new cat.Control.UserComands_onMessage.setmale(),
                new cat.Control.UserComands_onMessage.stats(),
            };
            ComandsAdmin_onMessage = new List<Command>()
            {
               new cat.Control.AdminComands_onMessage.adm(),
              new cat.Control.AdminComands_onMessage.h(),

            };
            ComandsAdmin_OnCallbackQuery = new List<Command>()
            {
                new cat.Control.Admin_Cmd_OnCallBackQuery.Chats(),
                new cat.Control.Admin_Cmd_OnCallBackQuery.Users(),
                new cat.Control.Admin_Cmd_OnCallBackQuery.Chat(),
                new cat.Control.Admin_Cmd_OnCallBackQuery.Setbat(),
                new cat.Control.Admin_Cmd_OnCallBackQuery.SetP(),
                new cat.Control.Admin_Cmd_OnCallBackQuery.User()
            };
        }

        public void OnMessage(TelegramBotClient sender, Message m)
        {
            try
            {
                if (!m.Text.Contains(Information.NameBot) && (m.Chat.Id != Information.AdminChatId)) return;
            }
            catch (Exception e)
            {
                new InfoService(e.ToString(),InfoService.TypeMess.Error,InfoService.TargetInfo.Telgram);
                return;
            } 

            string comd = m.Text.Split(Information.NameBot).First(); ;

            Command cmd =
                (m.Chat.Id == Information.AdminChatId ? ComandsAdmin_onMessage : Comands_onMessage).Find(t =>
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

            if (c.From.Id != Information.AdminChatId) return;
            string comd = c.Data.Split("_").First();
            Command cmd =
                (c.From.Id == Information.AdminChatId ? ComandsAdmin_OnCallbackQuery : null).Find(t =>
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