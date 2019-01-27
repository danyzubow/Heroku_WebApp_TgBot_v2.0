using System;
using System.Collections.Generic;
using Telegram.Bot;

namespace PorterOfChat.Bot.Model
{
    [Serializable]
    public class cChat : ICloneable
    {
        public List<cUser> users { get; set; }
        [NonSerialized] public bool LockGroupPidor = false;
        [NonSerialized] public bool LockGroupDad = false;
        public int Id { get; set; }
        public string Name { get; set; }
        public long Id_tg { get; set; }
        public string DatePidor { get; set; }
        public string Pidor { get; set; }
        public string FullPidor { get; set; }
        public string Dad { get; set; }
        public string FullDad { get; set; }
        public string DateDad { get; set; }


        public int CountMembers { get; set; }

        public cChat(List<cUser> userArg)
        {
            users = userArg;
        }

        public cChat()
        {
            users = new List<cUser>();
        }

        public bool UpdateInfo(Telegram.Bot.Types.Chat nowChat, TelegramBotClient client)
        {
            var needSave = false;
            var count = client.GetChatMembersCountAsync(Id_tg).Result;
            if (count != CountMembers)
            {
                CountMembers = count;
                needSave = true;
            }

            if (nowChat.Title != Name)
            {
                Name = nowChat.Title;
                needSave = true;
            }

            return needSave;
        }

        public override string ToString()
        {
            return $"{Name} [{Id}]";
        }

        public object Clone()
        {
            cChat cht = new cChat()
            {
                Id = this.Id,
                Name = this.Name,
                Id_tg = this.Id_tg,
                DatePidor = this.DatePidor,
                Pidor = this.Pidor,
                FullPidor = this.FullPidor,
                Dad = this.Dad,
                FullDad = this.FullDad,
                DateDad = this.DateDad,

            };

            cht.users = new List<cUser>();
            foreach (var t in this.users)
            {
                cht.users.Add((cUser)t.Clone());
            }
            return cht;
        }
    }


}