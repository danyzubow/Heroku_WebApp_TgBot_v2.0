
using cat.Bot;
using cat.Control.Admin_Cmd_OnCallBackQuery;
using Telegram.Bot.Types;

namespace cat.Control.AdminComands_onMessage
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
            await _Client.SendTextMessageAsync(Information.AdminChatId, outStr);
        }
    }

}


