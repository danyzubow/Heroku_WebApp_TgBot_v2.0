using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace PorterOfChat.Service
{
    /// <summary>  Создает и отправляет
    ///
    /// </summary>
    public class InfoService:BaseControl
    {
     
        public enum TargetInfo : byte
        {
            Consol = 1,
            Telgram,
            Both
        }

        public enum TypeMess : byte
        {
            Info = 1,
            Error
        }
  
        private const string sInfo = "InfoService:";
        private const string sError = "Error:";

        public InfoService(string mess, TypeMess typeMess = TypeMess.Info, TargetInfo target = TargetInfo.Both)
        {
            string tg, console;
            string typeCurrent = typeMess == TypeMess.Info ? sInfo : sError;
            switch (target)
            {
                case TargetInfo.Consol:
                    console = $"{DateTime.Now.ToString("G") + " | " + typeCurrent} " + mess;
                    Console.WriteLine(console);
                    break;
                case TargetInfo.Telgram:
                    tg = $"<i>    {typeCurrent}</i>\n<b>Time</b> {DateTime.Now.ToString("G")}\n{mess}";
                    try
                    {
                        SendTg(tg);
                    }
                    catch
                    {
                        new InfoService("Ошибка отправки в телеграм",TypeMess.Error,TargetInfo.Consol);
                    }
                    break;
                default:
                    console = $"{DateTime.Now.ToString("G") + " | " + typeCurrent} " + mess;
                    tg = $"<i>    {typeCurrent}</i>\n<b>Time</b> {DateTime.Now.ToString("G")}\n{mess}";
                    Console.WriteLine(console);
                    try
                    {
                        SendTg(tg);
                    }
                    catch
                    {
                        new InfoService("Ошибка отправки в телеграм", TypeMess.Error, TargetInfo.Consol);
                    }
                    break;
            }
        }

        async void SendTg(string mess)
        {
            SendTextMessageAsync("227950395", mess, ParseMode.Html);
        }

    }
}