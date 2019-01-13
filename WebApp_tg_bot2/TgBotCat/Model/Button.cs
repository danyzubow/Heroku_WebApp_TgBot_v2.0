using cat.Control;
using Telegram.Bot.Types.ReplyMarkups;


namespace cat.Model
{

    public class Button : InlineKeyboardButton
    {
  
        public Button(Command Command, params string[] arg_callBackData)
        {
            string str = "";
            if (arg_callBackData != null)
            {
                foreach (string t in arg_callBackData)
                    str += "_" + t;
            }

            CallbackData = Command.NameCommand + str;
            Text = "Назад/Обновить";
        }
        public Button(string name, Command Command, string callBackData)
        {
            CallbackData = Command.NameCommand + "_" + callBackData;
            Text = name;
        }
        public Button(string name, Command Command, params string[] arg_callBackData)
        {
            string str = "";
            foreach (string t in arg_callBackData)
                str += "_" + t;
            CallbackData = Command.NameCommand + str;
            Text = name;
        }
      
    }
}