using cat.Bot.Model;
using System.Collections.Generic;

namespace cat.Chat
{
    public interface IChat
    {
        void LoadAllChats();
        void Save_All();
        bool _Release { get; set; }
        List<cChat> _Chats { get; }
    }
}
