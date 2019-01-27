using PorterOfChat.Bot.Model;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace PorterOfChat.Chat
{
    public interface IChat
    {
        cChat GetChat(Message e);
        cChat GetChat(long Id);
        cUser GetUser_FromMess(Message e);
        cUser GetUser(int Id, Message e);
        void AddChat(cChat chat);
        void Remove(cChat chat);
        List<cChat> GetAllChats();
        void SaveAll();
    }
}
