using PorterOfChat.Bot;
using PorterOfChat.Bot.Model;
using PorterOfChat.Chat;
using PorterOfChat.Control;
using System;
using System.Collections.Generic;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using User = Telegram.Bot.Types.User;

namespace PorterOfChat
{

    public abstract class BaseControl
    {
        protected static TransporterCMD _transporterCmd = new TransporterCMD();
        protected static TelegramBotClient _Client; //  "@seadogs4_bot" "568147661:AAHEsAzNZAbW-t_eJlOviuWHPBb8J81EHts"
        protected static IChat Data;




        /// <summary>

        //protected static cChat FindGroupID(long id) //
        //{
        //    foreach (var el in _Chats)
        //        if (el.Id_tg == id)
        //            return el;
        //    return null;

        //}
        protected static long ChatIDs(Message e) => e.Chat.Id;
        protected static long UserIDs(Message e) => e.From.Id;
        protected static long ChatID(Message e) => e.Chat.Id;
        //protected static Bot.Model.cChat FindGroupID(Message e) //
        //{

        //    foreach (var el in _Chats)
        //        if (el.Id_tg == ChatID(e))
        //            return el;
        //    return null;
        //}



        protected static cUser FindUserFromDic(long chatID, int num) //
        {
            return Data.GetChat(chatID).users[num];
        }
        protected static cUser FindUser(long chatID, long id) //
        {
            return Data.GetChat(chatID).users.Find(t => t.Id_tg == id);
        }
        protected static cUser FindUserFromDic(Message e, int num) //
        {
            return Data.GetChat(ChatIDs(e)).users[num];
        }
        protected static bool ContainsUserFromDic(long chatId, long Id) //
        {
            if (Data.GetChat(chatId) == null) return false;
            var gGroup = Data.GetChat(chatId);
            foreach (var t in gGroup.users)
                if (t.Id_tg == Id)
                    return true;
            return false;
        }
        protected static bool ContainsUserFromDic(Message e) //
        {
            if (Data.GetChat(e) == null) return false;
            var gGroup = Data.GetChat(e);
            foreach (var t in gGroup.users)
                if (t.Id_tg == ChatIDs(e))
                    return true;
            return false;
        }

        protected static string setPidor(cChat tmpGroup, cUser User)
        {
            var random = new Random();
            try
            {
                User.CountPidor = User.CountPidor + 1;
            }
            catch
            {
                User.CountPidor = 1;
            }

            tmpGroup.DatePidor = DateTime.Now.AddHours(3).ToString("d");
            var emoj = $"{Settings.EmojPidor[random.Next(0, Settings.EmojPidor.Length)]}" +
                       $" {Settings.EmojPidor[random.Next(0, Settings.EmojPidor.Length)]}";

            var PidorOrHarlot = "ПІДОР";

            if (!User.GenderFemale)
                PidorOrHarlot = random.Next(0, 10) == 4 ? "ШЛЮХА" : PidorOrHarlot;
            else
                PidorOrHarlot = "ШЛЮХА";

            var PidorNew = $"{User.Name}-🌈{PidorOrHarlot} вахту приня" + (User.GenderFemale ? "ла " : "в ") + emoj;


            var PidorToday = $"{User.Name} -🌈{PidorOrHarlot} " + emoj;

            tmpGroup.Pidor = PidorToday;
            tmpGroup.FullPidor = $"{User.FullName}";
            return PidorNew;
        }

        protected static string setBatya(cChat tmpGroup, cUser User)
        {
            var random = new Random();
            try
            {
                User.CountDad = User.CountDad + 1;
            }
            catch
            {
                User.CountDad = 1;
            }

            tmpGroup.DateDad = DateTime.Now.AddHours(3).ToString("d");
            var emoj2 = $"{Settings.EmojPidor[random.Next(0, Settings.EmojPidor.Length)]}" +
                        $" {Settings.EmojPidor[random.Next(0, Settings.EmojPidor.Length)]}";

            var DadToday = $"{User.Name} -🔝БАТЯ дня {emoj2}";

            tmpGroup.Dad = DadToday;
            tmpGroup.FullDad = $"{User.FullName}";
            return DadToday;
        }

        protected static async void FindingPidor(cChat chat, string NewPidor)
        {
            await _Client.SendTextMessageAsync(chat.Id_tg, "Пошук вахтера по чату 🔍", ParseMode.Html);
            Thread.Sleep(1000);
            await _Client.SendTextMessageAsync(chat.Id_tg, "<i>3 - опитані учасники гейпараду 🗣</i>", ParseMode.Html);
            Thread.Sleep(1000);
            await _Client.SendTextMessageAsync(chat.Id_tg, "<i>2 - твоїх друзів пустили по колу 🎡</i>", ParseMode.Html);
            Thread.Sleep(1000);
            await _Client.SendTextMessageAsync(chat.Id_tg, "<i>1 - 🤵🏿 x 10 нігерів виїхали до тебе додому </i>",
                ParseMode.Html);
            Thread.Sleep(1500);
            await _Client.SendTextMessageAsync(chat.Id_tg, "<code>BU-DUM-TSS 🎲</code>", ParseMode.Html);
            Thread.Sleep(1500);
            await _Client.SendTextMessageAsync(chat.Id_tg, $"<b>{NewPidor}</b>", ParseMode.Html);
            await _Client.SendTextMessageAsync(chat.Id_tg, chat.FullPidor + " ⬅️ Link", ParseMode.Html);
            chat.LockGroupPidor = false;
        }

        protected static async void FindingDad(long chatID, cChat tmpGroup, string NewDad)
        {
            await _Client.SendTextMessageAsync(chatID, "Пошук БАТІ в чаті 🔍");
            Thread.Sleep(1000);
            await _Client.SendTextMessageAsync(chatID, "<i>3 - проведення виборів в Білорусії 🥔</i>", ParseMode.Html);
            Thread.Sleep(1000);
            await _Client.SendTextMessageAsync(chatID, "<i>2 - гортаєм журнал Forbes 💵</i>", ParseMode.Html);
            Thread.Sleep(1000);
            await _Client.SendTextMessageAsync(chatID, "<i>1 - чекаєм фотку в паспорті 🗿</i>", ParseMode.Html);
            Thread.Sleep(1500);
            await _Client.SendTextMessageAsync(chatID, "<code>BU-DUM-TSS 🎲</code>", ParseMode.Html);
            Thread.Sleep(1500);
            await _Client.SendTextMessageAsync(chatID, $"<b>{NewDad}</b>", ParseMode.Html);
            await _Client.SendTextMessageAsync(chatID, tmpGroup.FullDad + " ⬅️ Link");
            tmpGroup.LockGroupDad = false;
        }


    }
}