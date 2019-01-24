using PorterOfChat.Bot.Model;
using PorterOfChat.Model;
using Telegram.Bot.Types;


namespace PorterOfChat.Control.Admin_Cmd_OnCallBackQuery
{
    public class Chat : Command
    {
        public override string NameCommand { get; } = "Chat";

        public Button getButton(cChat chat)
        {
            return new Button(chat.Name, this, chat.Id_tg);
        }

        protected override void Execution(CallbackQuery c)
        {
            new Menu(new Chats().getButton(),
                c.Message.MessageId,
                $"Група <b>'{ThisChat.Name}</b>'" +
                $"[{ThisChat.Id_tg}] - " +
                $"\n'Підор' =<b>{ThisChat.FullPidor}</b>; [Последний раз(дата):{ThisChat.DatePidor}]" +
                $"\n'Батя' =<b>{ThisChat.FullDad}</b>; [Последний раз(дата):{ThisChat.DateDad}]" +
                $"\nКоличество учасников={ThisChat.users.Count}" +
                $"\nВсего учасников в чате={ThisChat.CountMembers}",
                new Users().getButton_Name("Список учасников", Argument[0].ToString())
            );

        }
    }
}