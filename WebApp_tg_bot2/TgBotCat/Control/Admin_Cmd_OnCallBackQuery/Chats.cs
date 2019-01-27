using System.Collections.Generic;
using PorterOfChat.Model;
using Telegram.Bot.Types;


namespace PorterOfChat.Control.Admin_Cmd_OnCallBackQuery
{
    public class Chats : Command
    {
        public override string NameCommand { get; } = "Chats";

        public static Button[] get_chats_Buttons()
        {
            List<Button> buttons = new List<Button>();
            Chat chat = new Chat();
            foreach (var t in Data.GetAllChats())
            {
                buttons.Add(chat.getButton(t));
            }

            return buttons.ToArray();

        }

        protected override async void Execution(CallbackQuery c)
        {

            if (c == null)
                new Menu(getButton(), get_chats_Buttons());
            else
                new Menu(getButton(), c.Message.MessageId, "", get_chats_Buttons());
        }
    }
}