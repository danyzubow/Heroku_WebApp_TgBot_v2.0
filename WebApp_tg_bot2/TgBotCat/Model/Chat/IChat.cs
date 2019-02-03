using PorterOfChat.Bot.Model;
using System.Collections.Generic;
using Telegram.Bot.Types;
using WebApp_tg_bot2.TgBotCat.Model;

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
        void AddMessage(MessageLog msg);
        List<MessageLog> GetMessageLogs();
        void SaveAll();
    }
}
