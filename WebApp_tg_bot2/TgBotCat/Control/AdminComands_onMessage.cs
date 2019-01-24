using PorterOfChat.Bot;
using PorterOfChat.Control.Admin_Cmd_OnCallBackQuery;
using Telegram.Bot.Types;

namespace PorterOfChat.Control.AdminComands_onMessage
{
    class adm : Command
    {
        public override string NameCommand { get; } = "/adm";
        protected override async void Execution(Message m)
        {
     
            new Chats().Exec(null, (CallbackQuery)null);
        }
    }
    class h : Command
    {
        public override string NameCommand { get; } = "/h";
        protected override async void Execution(Message m)
        {
            string outStr = "Command:\n/data";
            await _Client.SendTextMessageAsync(Settings.AdminChatId, outStr);
        }
    }

}


