using cat.Bot.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace cat.Control.UserComands_onMessage
{
    public class loser : Command
    {
        public override string NameCommand { get; } = "/pidor";

        protected override async void Execution(Message m)
        {
            if (FindGroupID(m) == null)
            {
                await Tclient.SendTextMessageAsync(ChatID(m), "Список пустий🐷");
                return;
            }

            var random = new Random();
            if (ThisChat.LockGroupPidor) return;
            if (ThisChat.users.Count == 0)
            {
                await Tclient.SendTextMessageAsync(chatID, "Список пустий🐷");
                return;
            }

            var refreshPidor = ThisChat.DatePidor == null;

            if (!refreshPidor)
                refreshPidor = ThisChat.DatePidor !=
                DateTime.Now.AddHours(3).ToString("d");
            if (refreshPidor)

            {
                var rand = random.Next(0, ThisChat.users.Count);
                thisUser = FindUserFromDic(m, rand);

                var PidorNew = setPidor(ThisChat, thisUser);
                ThisChat.LockGroupPidor = true;
                new Task(() => FindingPidor(ThisChat, PidorNew)).Start();
            }
            else
            {
                await Tclient.SendTextMessageAsync(chatID,
                    $"⚠️<i>Сьогодні на вахті</i>⚠️\n <b>{ThisChat.Pidor}</b>", ParseMode.Html);
                await Tclient.SendTextMessageAsync(chatID, ThisChat.FullPidor + " ⬅️ Link");
            }


        }
    }
    public class reg : Command
    {
        public override string NameCommand { get; } = "/reg";
        protected override async void Execution(Message m)
        {
            if (ThisChat == null) //(!ContainsGroupFromDic(e.Message.cChat.Id.ToString()))
            {
                string nameGroup;
                if (m.Chat.Title == null)
                    nameGroup = m.Chat.FirstName + "_" + m.Chat.LastName;
                else
                    nameGroup = m.Chat.Title;
                var newChat = new cChat(new List<cUser>())
                {
                    Id = ChatIDs(m),
                    Name = nameGroup
                };
                _Chats.Add(newChat);
            }

            if (ContainsUserFromDic(ChatIDs(m), m.From.Id.ToString()))
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
                    Id = m.From.Id.ToString(),
                    FullName = sName,
                    Name = m.From.FirstName,
                    CountPidor = "0",
                    CountDad = "0"
                };
                FindGroupID(m).users.Add(newUser);

                await Tclient.SendTextMessageAsync(chatID,
                    sName + " кидає мило на підлогу🏋️\n /setfemale - Стати подругой👠" +
                    "\n /setmale - Стати пациком💪🏻");
            }
        }
    }
    public class setfemale : Command
    {
        public override string NameCommand { get; } = "/setfemale";
        protected override async void Execution(Message m)
        {
            if (!ContainsUserFromDic(m))
            {
                await Tclient.SendTextMessageAsync(chatID,
                    m.From.FirstName + " ти не в темі🤔");
                return;
            }

            thisUser = FindGroupID(m).users.Find(t =>
                t.Id == UserIDs(m));
            thisUser.GenderFemale = true;
            await Tclient.SendTextMessageAsync(chatID, thisUser.Name + " стає подругой👠");
        }
    }
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

            thisUser = FindGroupID(m).users.Find(t =>
                t.Id == UserIDs(m));
            thisUser.GenderFemale = false;
            await Tclient.SendTextMessageAsync(chatID, thisUser.Name + " стає пациком💪🏻");
        }
    }
    public class leave : Command
    {
        public override string NameCommand { get; } = "/leave";
        protected override async void Execution(Message m)
        {

            if (FindGroupID(m) == null)
            {
                await Tclient.SendTextMessageAsync(chatID, "список пустий😕");
            }
            else
            {
                var sName = m.From.FirstName + " " + m.From.LastName;
                if (ContainsUserFromDic(m))
                {
                    thisUser = FindGroupID(m).users.Find(t =>
                        t.Id == UserIDs(m));
                    FindGroupID(m).users.Remove(thisUser);
                    await Tclient.SendTextMessageAsync(chatID, sName + " ліває🚮");
                }
                else
                {
                    await Tclient.SendTextMessageAsync(chatID, sName + " ти не в темі🤔");
                }
            }


        }
    }
    public class batya : Command
    {
        public override string NameCommand { get; } = "/batya";
        protected override async void Execution(Message m)
        {

            if (FindGroupID(m) == null)
            {
                await Tclient.SendTextMessageAsync(chatID, "Список пустий🐷");
                return;
            }

            var random = new Random();
            if (ThisChat.LockGroupDad) return;
            if (ThisChat.users.Count == 0)
            {
                await Tclient.SendTextMessageAsync(chatID, "Список пустий🐷");
                return;
            }

            var refreshPidor = ThisChat.DateDad == null;

            if (!refreshPidor)
                refreshPidor = ThisChat.DateDad !=
                               DateTime.Now.AddHours(3).ToString("d");
            if (refreshPidor)

            {
                var rand = random.Next(0, ThisChat.users.Count);
                thisUser = FindUserFromDic(ChatIDs(m), rand);

                var DadToday = setBatya(ThisChat, thisUser);
                ThisChat.LockGroupDad = true;
                new Task(() => FindingDad(chatID, ThisChat, DadToday)).Start();
            }
            else
            {
                await Tclient.SendTextMessageAsync(chatID, $"⚠️<b>Сьогодні {ThisChat.Dad}</b>",
                    ParseMode.Html);
                await Tclient.SendTextMessageAsync(chatID, ThisChat.FullDad + " ⬅️ Link");
            }
        }
    }
    public class stats : Command
    {
        public override string NameCommand { get; } = "/stats";
        protected override async void Execution(Message m)
        {

            if (FindGroupID(m) == null)
            {
                await Tclient.SendTextMessageAsync(chatID, "Список пустий🐷");
                return;
            }

            if (ThisChat.users.Count == 0)
            {
                await Tclient.SendTextMessageAsync(chatID, "Список пустий🐷");
                return;
            }

            var stats = "<b>Результати 🌈ВАХТЕРИ Дня</b>";
            var count = 1;
            foreach (var t in ThisChat.users)
            {
                stats += $"\n<b>{count}</b>)" + t.FullName + " - " + t.CountPidor +
                         " раз(а)";
                count++;
            }

            var statsDat = "\n<b>Результати 🔝БАТЯ дня</b>";
            count = 1;
            foreach (var t in ThisChat.users)
            {
                statsDat += $"\n<b>{count}</b>)" + t.FullName + " - " + t.CountDad +
                            " раз(а)";
                count++;
            }

            await Tclient.SendTextMessageAsync(chatID, stats, ParseMode.Html);
            await Tclient.SendTextMessageAsync(chatID, statsDat, ParseMode.Html);

        }
    }
    public class sheriff : Command
    {
        public override string NameCommand { get; } = "/sheriff";
        protected override async void Execution(Message m)
        {

            if (FindGroupID(m) == null)
            {
                await Tclient.SendTextMessageAsync(chatID, "Список пустий🐷");
                return;
            }

            var tmpGroup2 = FindGroupID(m);
            if (tmpGroup2.users.Count == 0)
            {
                await Tclient.SendTextMessageAsync(chatID, "Список пустий🐷");
                return;
            }

            var stats2 = "Головний по 🌈вахті";
            var users = new List<cUser>();
            var max = 0;
            foreach (var t in tmpGroup2.users)
            {
                if (int.Parse(t.CountPidor) == max)
                {
                    users.Add(t);
                    max = int.Parse(t.CountPidor);
                }

                if (int.Parse(t.CountPidor) > max)
                {
                    users.Clear();
                    users.Add(t);
                    max = int.Parse(t.CountPidor);
                }
            }


            foreach (var user in users)
                stats2 += $"\n🥇" + user.FullName + " - " + user.CountPidor +
                          " раз(а)";


            await Tclient.SendTextMessageAsync(chatID, stats2);

        }
    }

}