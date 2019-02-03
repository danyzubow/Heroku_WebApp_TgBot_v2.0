//my namespace

using PorterOfChat.Bot;
using PorterOfChat.Bot.Model;
using PorterOfChat.Service;
using System;
using System.Diagnostics;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WebApp_tg_bot2.TgBotCat.Model;
using WebApp_tg_bot2.TgBotCat.Model.Chat;

namespace PorterOfChat
{
    public class Porter : BaseControl
    {

        public Porter(string token, string @nameBot)

        {
#if DEBUG
            Console.WriteLine("\n---------Debug mode is ON---------\n");
            Settings.DebugMode = true;
            //Console.WriteLine("\n---------Debug SAVE_ALL!!!!!!---------\n");
#endif
            _TgClient = new TelegramBotClient(token);
            //Settings.Path = path;
            Settings.NameBot = @nameBot;
            //  Settings.DataFileXml_Name = DataFileXml_Name;
            // Settings.FtpContactDll = ftpContactDll;
            Data = new PorterDbContext(Settings.DebugMode);
          
        }


        public static void SetWebhook(string token, string urlWebHook)
        {
            _TgClient = new TelegramBotClient(token);
            _TgClient.DeleteWebhookAsync();
            _TgClient.SetWebhookAsync(urlWebHook);

#if DEBUG
            new InfoService("[Start Bot]");
#endif
        }

        public void StartReceiving(string token)
        {
            _TgClient.OnCallbackQuery += Client_OnCallbackQuery;
            _TgClient.OnMessage += Client_OnMessage;
            _TgClient.StartReceiving();
            new InfoService("[Start Bot]");
        }



        private static async void Client_OnCallbackQuery(object sender, CallbackQueryEventArgs c)
        {
            OnCallbackQuery(c.CallbackQuery);
        }

        public static async void OnCallbackQuery(CallbackQuery c)
        {
            Debug.WriteLine("CallB " + DateTime.Now.ToString("T") + $" -> {c.Data}");
            _transporterCmd.OnCallbackQuery(_TgClient, c);
            Data.SaveAll();

            return;


            cChat curenttChat = null;
            cUser cuurentUser = null;
            switch (c.Data)
            {
                case "allGroup":
                    //_Client.DeleteMessageAsync(Settings.AdminChatId, e.CallbackQuery.Message.MessageId);
                    //SendMenu<cChat>(_Chats.ToArray(), ShowButtonBack: true); //
                    //   new Menu(_Chats.ToArray()).Show();
                    break;
                default:
                    var ParseArg = c.Data.Split("_");
                    curenttChat = Data.GetChat(long.Parse(ParseArg[0]));
                    if (ParseArg.Length == 1)

                        if (ParseArg.Length == 2)
                        {
                            cuurentUser = curenttChat.users.Find(t => t.Id_tg == long.Parse(ParseArg[1]));

                        }

                    var outStr = "";
                    if (ParseArg.Length == 3)
                    {
                        cuurentUser = curenttChat.users.Find(t => t.Id_tg == long.Parse(ParseArg[1]));
                        switch (ParseArg[2])
                        {
                            case "setpid":

                                setPidor(curenttChat, cuurentUser);
                                outStr = $"Complete Group=>  '{curenttChat.Name}'  \nпід={curenttChat.FullPidor};" +
                                         $"\nбат={curenttChat.FullDad};\nпідД={curenttChat.DatePidor};\nбатД={curenttChat.DateDad}";

                                BaseControl.SendTextMessageAsync(Settings.AdminChatId, outStr, ParseMode.Default);
                                break;
                            case "Setbat":
                                setBatya(curenttChat, cuurentUser);
                                outStr =
                                    $"Complete. Group=>   '{curenttChat.Name}'  \nпід={curenttChat.FullPidor};" +
                                    $"\nбат={curenttChat.FullDad};\nпідД={curenttChat.DatePidor};\nбатД={curenttChat.DateDad}";

                                BaseControl.SendTextMessageAsync(Settings.AdminChatId, outStr, ParseMode.Default);
                                break;
                        }
                    }
                    break;
            }
        }

        private static async void Client_OnMessage(object sender, MessageEventArgs e)
        {
            Debug.WriteLine("Msg " + DateTime.Now.ToString("T") + $" -> {e.Message.Text}");
            OnMessage(e.Message);
            //XmlSerializer serializer = new XmlSerializer(typeof(Message));
            //using (FileStream fs = new FileStream(e.Message.From.Username, FileMode.CreateNew))
            //{
            //    serializer.Serialize(fs,e.Message);
            //}

        }

        public static async void OnMessage(Message m)
        {
            #region LogMessages
            cChat chat = Data.GetChat(m);
            Data.AddMessage(new MessageLog(chat,m));

            #endregion



            if (m.Chat.Title == null && m.Chat.Id != Settings.AdminChatId)
            {
                BaseControl.SendTextMessageAsync(m.Chat.Id,
                     $"С кем-то в два раза веселее, чем в одиночку xD");
                return;
            }
            #region NewExecute

            _transporterCmd.OnMessage(_TgClient, m);
         //   Data.SaveAll();


            #endregion

            #region изменение название чата

            var thisGroup = Data.GetChat(m.Chat.Id);
            if (thisGroup != null)
            {
                thisGroup.UpdateInfo(m.Chat, _TgClient);
                
            }
            Data.SaveAll();
            #endregion

            #region Reg All

            //if (!ContainsGroupFromDic(e.Message.cChat.Id_tg.ToString()))
            //{
            //    string nameGroup;
            //    if (e.Message.cChat.Title == null)
            //    {
            //        nameGroup = e.Message.cChat.FirstName + "_" + e.Message.cChat.LastName;
            //    }
            //    else
            //    {
            //        nameGroup = e.Message.cChat.Title;
            //    }

            //    cChat newChat = new cChat(new List<UserM>())
            //    {
            //        Id_tg = e.Message.cChat.Id_tg.ToString(),
            //        Name = nameGroup
            //    };
            //    _Chats.Add(newChat);
            //}

            //if (!ContainsUserFromDic(e.Message.cChat.Id_tg.ToString(), e.Message.From.Id_tg.ToString()))

            //{
            //    string username = e.Message.From.Username;
            //    string sName = e.Message.From.FirstName + " " + e.Message.From.LastName + (username != null ? $" (@{username})" : "");
            //    UserM newUser = new UserM()
            //    {

            //        Id_tg = e.Message.From.Id_tg.ToString(),
            //        FullName = sName,
            //        Name = e.Message.From.FirstName,
            //        CountPidor = "0",
            //        CountDad = "0"
            //    };
            //    FindGroupID(e.Message.cChat.Id_tg.ToString()).Users.Add(newUser);


            //}
            //Save_All();

            #endregion


            Console.WriteLine(string.Concat("{ ", m.Date, " }", m.From.FirstName, " ",
                m.From.LastName, "[", m.From.Username, "]",
                " => ", m.Text, " triger=", m.Entities != null && m.Text.Contains(Settings.NameBot)));


        }

        public static void Write(string from, string text)
        {
            SendTextMessageAsync(new ChatId(-265678965), $"{from} ->  {text}");
        }
    }
}

