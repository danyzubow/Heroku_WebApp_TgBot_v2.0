using cat.Bot;
using cat.Bot.Model;
using cat.Model;
using cat.Service;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace cat.Control
{
    public abstract class Command : BaseControl
    {
        public abstract string NameCommand { get; }

        private Message EE_Message;
        private CallbackQuery EE_CallBack;
        protected TelegramBotClient Tclient;

        public Button getButton(params string[] arg_callBackData)
        {
            return new Button(this, arg_callBackData);
        }
        public Button getButton_Name(string Name, params string[] arg_callBackData)
        {

            return new Button(Name, this, arg_callBackData);
        }

        public Command()
        {
            if (NameCommand == null)
            {
                new InfoService("Команда без имени", InfoService.TypeMess.Error, InfoService.TargetInfo.Consol);
            }
        }
        //
        private cChat ThisChat_;

        protected cChat ThisChat
        {
            set { ThisChat_ = value; }
            get
            {
                if (ThisChat_ == null)
                {
                    ThisChat_ = EE_Message != null ? FindGroupID(ChatIDs(EE_Message)) :
                        FindGroupID(Argument[0]);
                }
                return ThisChat_;
            }
        }

        cUser ThisUser_;

        protected cUser thisUser
        {
            set { ThisUser_ = value; }
            get
            {
                if (ThisUser_ == null)
                {
                    ThisUser_ = EE_Message != null ? null : FindUser(Argument[0], Argument[1]);
                }
                return ThisUser_;
            }
        }

        private long chatID_;

        protected long chatID
        {
            set { chatID_ = value; }
            get
            {
                if (chatID_ == 0)
                {
                    chatID_ = ChatID(EE_Message);
                }

                return chatID_;
            }
        }

        void CleanAll()
        {
            this.Argument_ = null;
            this.ThisChat_ = null;
            this.ThisUser_ = null;
            this.chatID_ = 0;
            Current_Command_ = null;
        }
        //
        public void Exec(object sender, Message m)
        {
            CleanAll();
            EE_Message = m;
            Tclient = (TelegramBotClient)sender;
            Execution(EE_Message);
        }
        public void Exec(object sender, CallbackQuery c)
        {

            CleanAll();
            EE_CallBack = c;
            Tclient = (TelegramBotClient)sender;
            Execution(EE_CallBack);
        }

        protected virtual void Execution(Message m)
        {
            new InfoService($"Команда \'{NameCommand}\' {{MessageEvent}} не имеет реализации", InfoService.TypeMess.Error);
        }

        protected virtual void Execution(CallbackQuery c)
        {
            new InfoService($"Команда \'{NameCommand}\' {{CallbackQueryEventArgs}} не имеет реализации", InfoService.TypeMess.Error);
        }
        private string[] Argument_;
        /// <summary>
        /// Первый агрумент то индексу 0;
        /// 1 - чат
        /// 2 - user
        /// </summary>
        public string[] Argument
        {
            set { Argument_ = value; }
            get
            {
                if (Argument_ == null)
                {
                    if (EE_CallBack == null && EE_Message == null) return null;
                    Argument_ = (EE_Message != null ? current_Command.Split("_").Where((p, i) => i != 0).ToArray()
                        : EE_CallBack?.Data?.Split("_")).Where((p, i) => i != 0).ToArray();

                }

                return Argument_;
            }
        }

        private string Current_Command_;

        protected string current_Command
        {
            set { Current_Command_ = value; }
            get
            {
                if (Current_Command_ == null)
                {
                    Current_Command_ = EE_Message.Text.Split(Information.NameBot).First();

                }

                return Current_Command_;
            }
        }

        protected async void send_Message_html(string text, string id)
        {
            int iD = 0;
            try
            {
                iD = int.Parse(id);
            }
            catch
            {
                new InfoService($"Команда{current_Command} функція 'send_Message_html' ---->int.Parse", InfoService.TypeMess.Error);
                return;
            }
            await _Client.SendTextMessageAsync(iD, text, ParseMode.Html);
        }
        protected async void send_Message_html_admin(string text)
        {
            await _Client.SendTextMessageAsync(Information.AdminChatId, text, ParseMode.Html);
        }
    }


}