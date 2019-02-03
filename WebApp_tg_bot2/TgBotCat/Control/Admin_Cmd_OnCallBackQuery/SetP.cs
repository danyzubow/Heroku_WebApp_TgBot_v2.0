using System.Threading.Tasks;
using PorterOfChat.Bot.Model;
using PorterOfChat.Service;
using Telegram.Bot.Types;


namespace PorterOfChat.Control.Admin_Cmd_OnCallBackQuery
{
    public class SetP : Command
    {
        public override string NameCommand { get; } = "SetP";

        protected override  void Execution(CallbackQuery c)
        {

         if (ThisChat.FullPidor == ""|| ThisChat.FullPidor == null)
            {
                new InfoService($"В чате '{ThisChat.Name}' еще нет 'пидора' ");
                return;
            }

            cUser c_current_user = ThisChat.users.Find(t =>
                t.FullName == ThisChat.FullPidor);
            c_current_user.CountPidor = c_current_user.CountPidor - 1;

            var newPidor = setPidor(ThisChat, thisUser);
           new Task(() => FindingPidor( ThisChat, newPidor)).Start();
            string outStr =
                $"<b>Complete.</b> Чат=> <b>* ) '{ThisChat.Name}'</b> [{ThisChat.Id_tg}] - " +
                $"\n'Підор' =<b>{ThisChat.FullPidor}</b>; [Последний раз(дата):{ThisChat.DatePidor}]" +
                $"\n'Батя' =<b>{ThisChat.FullDad}</b>; [Последний раз(дата):{ThisChat.DateDad}]";
            new Menu
                (
                new User().getButton_Name("Ок", Argument[0], Argument[1]),
                c.Message.MessageId,
                outStr
                );


            
        }
    }
}