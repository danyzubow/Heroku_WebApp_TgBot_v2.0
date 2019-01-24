using System;
using System.Collections.Generic;
using Telegram.Bot;

namespace PorterOfChat.Bot.Model
{
    [Serializable]
    public class cChat
    {
        public  List<cUser> users { get; set; } 
        [NonSerialized] public bool LockGroupPidor = false;
        [NonSerialized] public bool LockGroupDad = false;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Id_tg { get; set; }
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
            users=new List<cUser>();
        }

        public bool UpdateInfo(Telegram.Bot.Types.Chat nowChat, TelegramBotClient client)
        {
            var needSave = false;
            var count = client.GetChatMembersCountAsync(long.Parse(Id_tg)).Result;
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
    }


}