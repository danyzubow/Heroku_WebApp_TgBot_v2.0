using cat.Bot;
using cat.Model;
using System;
using System.Diagnostics;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace cat.Control
{


    public class Menu : BaseControl
    {
       private int? Del_Last_Message;
        private string Title;


        private int CountColumn;
        private InlineKeyboardButton[][] ButtonsOfMenu;

        void CreateTableColum(Button[] itemsMenu, Button backButton = null)
        {
            CountColumn = itemsMenu.Length + (backButton != null ? 1 : 0);
            ButtonsOfMenu = new InlineKeyboardButton[CountColumn][];
            int i = 0;
            if (backButton != null)
            {
                ButtonsOfMenu[0] = new InlineKeyboardButton[1]
                {
                    backButton
                };
                i = 1;
            }

            for (int i_item = 0; i < CountColumn; i++, i_item++)
            {
                ButtonsOfMenu[i] = new InlineKeyboardButton[1]
                {
                   itemsMenu[i_item]
                };
            }
            Show();
            OutMenu(itemsMenu, backButton);
        }
        [Conditional("DEBUG")]
        void OutMenu(Button[] itemsMenu, Button backButton = null)
        {
            String outStr = backButton == null ? "" : $"{backButton.Text}->{backButton.CallbackData}";
            foreach (var t in itemsMenu)
            {
                outStr += "\n" + $"{t.Text}->{t.CallbackData}";
            }

            Console.WriteLine("-------Menu button-------\n" + outStr + "\n--------------------");

        }
      
        public Menu(Button backButton, params Button[] itemsForMenu)
        {

            Del_Last_Message = -1;
            Title = "";

            CreateTableColum(itemsForMenu, backButton);
        }
        public Menu(Button backButton, int? del_Last_Message = null, string title = "", params Button[] itemsForMenu)
        {

            Del_Last_Message = del_Last_Message;
            Title = title;

            CreateTableColum(itemsForMenu, backButton);
        }

        public Menu(Button backButton, Button[] itemsForMenu, int? del_Last_Message = null, string title = "")
        {

            Del_Last_Message = del_Last_Message;
            Title = title;

            CreateTableColum(itemsForMenu, backButton);
        }


        private const string Pre_Title = "<b>------------------</b>\n<b>&lt&lt  Меню   &gt&gt</b>\n<b>------------------</b>\n";
        async void Show()
        {

            InlineKeyboardMarkup inlinemMarkup = new InlineKeyboardMarkup(ButtonsOfMenu);
            if (Del_Last_Message != null) _Client.DeleteMessageAsync(Information.AdminChatId, (int)Del_Last_Message);


            await _Client.SendTextMessageAsync(Information.AdminChatId, Pre_Title + Title, ParseMode.Html,
                 false, false, 0, inlinemMarkup);




        }

    }



}
