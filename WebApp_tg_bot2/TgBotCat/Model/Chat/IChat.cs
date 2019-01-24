using System.Collections.Generic;
using PorterOfChat.Bot.Model;

namespace PorterOfChat.Chat
{
    public interface IChat
    {
        void LoadAllChats();
        void Save_All();
        bool _Release { get; set; }
        List<cChat> _Chats { get; }
    }
}
