using System;
using System.Collections.Generic;
using Telegram.Bot;

namespace cat.Bot.Model
{
    [Serializable]
    public class cChat
    {
        public List<cUser> users = new List<cUser>();
        [NonSerialized] public bool LockGroupPidor = false;
        [NonSerialized] public bool LockGroupDad = false;

        public string Name;
        public string Id;
        public string DatePidor;
        public string Pidor;
        public string FullPidor;
        public string Dad;
        public string FullDad;
        public string DateDad;


        public int CountMembers;

        public cChat(List<cUser> userArg)
        {
            users = userArg;
        }

        public cChat()
        {
        }

        public bool UpdateInfo(Telegram.Bot.Types.Chat nowChat, TelegramBotClient client)
        {
            var needSave = false;
            var count = client.GetChatMembersCountAsync(long.Parse(Id)).Result;
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