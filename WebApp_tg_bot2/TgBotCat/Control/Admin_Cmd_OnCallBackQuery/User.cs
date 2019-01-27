using PorterOfChat.Bot.Model;
using PorterOfChat.Model;
using Telegram.Bot.Types;


namespace PorterOfChat.Control.Admin_Cmd_OnCallBackQuery
{
    public class User : Command
    {
        public override string NameCommand { get; } = "User";

        public Button getButton(cChat chat,cUser user)
        {
            return new Button(user.FullName, this,chat.Id_tg.ToString(), user.Id_tg.ToString());
        }

        protected override void Execution(CallbackQuery c)
        {
            new Menu
            (
                new Users().getButton(Argument[0]),
                new Button[]
                {
                    new Button("Сделать пдр (простая подмена)", new SetP(), Argument[0], Argument[1]),
                    new Button("Зделать батей (простая подмена)", new Setbat(), Argument[0], Argument[1]),
                },
                c.Message.MessageId,
                $"<b>{thisUser.FullName}</b> з чата '{ThisChat.Name}'\n" +
                $"Батя ={thisUser.CountDad} раз\n" +
                $"Підр ={thisUser.CountPidor} раз"
            );
         
        }
    }
}