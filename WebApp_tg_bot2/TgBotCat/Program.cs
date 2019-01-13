//my namespace

using cat.Bot;
using cat.Bot.Model;
using cat.Chat;
using cat.Service;
using System;
using System.Diagnostics;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.IO;
namespace cat
{
    public class Program : BaseControl
    {
        private static bool _Release;
        private static IChat _Chatdata;

        public static async void Initialization(string arg)
        {
            if (_Client == null) return;
            Information.DataFileXml_Name = "Saves.xml";
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                _Client = new TelegramBotClient("521500060:AAH4Cj8XkwG0BpyDPy_a-hFN5LtFC5IC0sM");
               // _Client = new TelegramBotClient("568147661:AAHEsAzNZAbW-t_eJlOviuWHPBb8J81EHts");
                // _Client = new TelegramBotClient("568147661:AAHEsAzNZAbW-t_eJlOviuWHPBb8J81EHts");
                // new InfoService($"windows {Environment.CurrentDirectory}");

                // _Client.SetWebhookAsync("        telegrambot228.herokuapp.com/home/update");
                Information.NameBot = "@PorterOfChatBot";
                Information.Path = Environment.CurrentDirectory+"\\wwwroot\\" + "\\Data\\";

                _Release = false;
                _Client.DeleteWebhookAsync();
                _Client.SetWebhookAsync("https://fb28f594.ngrok.io/home/update");
                //_Client.OnInlineQuery += Client_OnInlineQuery;
                //  _Client.OnUpdate += Client_OnUpdate;
                //
                //Information.FtpContactDll =
                //    ""; 
                // @"G:\cat-master\cat\Data\ftpcontact";  // @"E:\ЛП\С#\Ftp-teste\ftp-contact\bin\Debug\netcoreapp2.0\ftp-contact";

                //ChatData.ChatData.DownloadSaves();
            }
            else
            {
               
                _Client = new TelegramBotClient("568147661:AAHEsAzNZAbW-t_eJlOviuWHPBb8J81EHts");
               // new InfoService($"linux {Environment.CurrentDirectory}");
                Information.NameBot = "@seadogs4_bot";
                Information.Path = Environment.CurrentDirectory+"//";// + "/wwwroot/"  + "/Data/";
                Information.FtpContactDll = "ftpcontact.dll"; //unix .dll обезательно
                _Release = false;
                System.IO.File.Copy($"{Environment.CurrentDirectory}/wwwroot/Data/ftpcontact.dll", $"{Environment.CurrentDirectory}/ftpcontact.dll");
                ////ChatData.ChatData.DownloadSaves();
                _Client.DeleteWebhookAsync();
                _Client.SetWebhookAsync("https://telegrambot228.herokuapp.com/home/update");
            }

          
            //_Client.SetWebhookAsync("https://fb28f594.ngrok.io/home/update");
            //new InfoService($"{Environment.CurrentDirectory}");
            _Chatdata = new Chat.ChatData(_Release);
            _Chats = _Chatdata._Chats;

            new InfoService("Time(+3) = " + DateTime.Now.AddHours(3).ToString("G"));

            _Client.OnCallbackQuery += Client_OnCallbackQuery;
            _Client.OnMessage += Client_OnMessage;
           // _Client.StartReceiving();

        }

        private static async void Client_OnCallbackQuery(object sender, CallbackQueryEventArgs c)
        {
            OnCallbackQuery(c.CallbackQuery);
        }

        public static async void OnCallbackQuery(CallbackQuery c)
        {
            Debug.WriteLine("CallB " + DateTime.Now.ToString("T")+$" -> {c.Data}");
            _transporterCmd.OnCallbackQuery(_Client, c);
            _Chatdata.Save_All();
            return;


            cChat curenttChat = null;
            cUser cuurentUser = null;
            switch (c.Data)
            {
                case "allGroup":
                    //_Client.DeleteMessageAsync(Information.AdminChatId, e.CallbackQuery.Message.MessageId);
                    //SendMenu<cChat>(_Chats.ToArray(), ShowButtonBack: true); //
                    //   new Menu(_Chats.ToArray()).Show();
                    break;
                default:
                    var ParseArg = c.Data.Split("_");
                    curenttChat = FindGroupID(ParseArg[0]);
                    if (ParseArg.Length == 1)

                        if (ParseArg.Length == 2)
                        {
                            cuurentUser = curenttChat.users.Find(t => t.Id == ParseArg[1]);

                        }

                    var outStr = "";
                    if (ParseArg.Length == 3)
                    {
                        cuurentUser = curenttChat.users.Find(t => t.Id == ParseArg[1]);
                        switch (ParseArg[2])
                        {
                            case "setpid":

                                setPidor(curenttChat, cuurentUser);
                                outStr = $"Complete Group=>  '{curenttChat.Name}'  \nпід={curenttChat.FullPidor};" +
                                         $"\nбат={curenttChat.FullDad};\nпідД={curenttChat.DatePidor};\nбатД={curenttChat.DateDad}";

                                await _Client.SendTextMessageAsync(Information.AdminChatId, outStr, ParseMode.Default);
                                break;
                            case "Setbat":
                                setBatya(curenttChat, cuurentUser);
                                outStr =
                                    $"Complete. Group=>   '{curenttChat.Name}'  \nпід={curenttChat.FullPidor};" +
                                    $"\nбат={curenttChat.FullDad};\nпідД={curenttChat.DatePidor};\nбатД={curenttChat.DateDad}";

                                await _Client.SendTextMessageAsync(Information.AdminChatId, outStr, ParseMode.Default);
                                break;
                        }
                    }
                    break;
            }
        }

        private static async void Client_OnMessage(object sender, MessageEventArgs e)
        {
            Debug.WriteLine("Msg "+ DateTime.Now.ToString("T") + $" -> {e.Message.Text}");
            OnMessage(e.Message);
            //XmlSerializer serializer = new XmlSerializer(typeof(Message));
            //using (FileStream fs = new FileStream(e.Message.From.Username, FileMode.CreateNew))
            //{
            //    serializer.Serialize(fs,e.Message);
            //}
        }

        public static async void OnMessage(Message m)
        {
            if (m.Chat.Title == null && m.Chat.Id != Information.AdminChatId)
            {
                await _Client.SendTextMessageAsync(m.Chat.Id,
                    $"Скоріше за все ти Тарас-Підарас, а якщо ти {m.From.FirstName} то ти тож підор, зміни ім'я на {m.From.FirstName}-підор xD");
                return;
            }
            #region NewExecute

            _transporterCmd.OnMessage(_Client, m);
            _Chatdata.Save_All();


            #endregion

            #region изменение название чата

            var thisGroup = FindGroupID(m.Chat.Id.ToString());
            if (thisGroup != null)
            {
                thisGroup.UpdateInfo(m.Chat, _Client);
                _Chatdata.Save_All();
            }

            #endregion

            #region Reg All

            //if (!ContainsGroupFromDic(e.Message.cChat.Id.ToString()))
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
            //        Id = e.Message.cChat.Id.ToString(),
            //        Name = nameGroup
            //    };
            //    _Chats.Add(newChat);
            //}

            //if (!ContainsUserFromDic(e.Message.cChat.Id.ToString(), e.Message.From.Id.ToString()))

            //{
            //    string username = e.Message.From.Username;
            //    string sName = e.Message.From.FirstName + " " + e.Message.From.LastName + (username != null ? $" (@{username})" : "");
            //    UserM newUser = new UserM()
            //    {

            //        Id = e.Message.From.Id.ToString(),
            //        FullName = sName,
            //        Name = e.Message.From.FirstName,
            //        CountPidor = "0",
            //        CountDad = "0"
            //    };
            //    FindGroupID(e.Message.cChat.Id.ToString()).Users.Add(newUser);


            //}
            //Save_All();

            #endregion


            Console.WriteLine(string.Concat("{ ", m.Date, " }", m.From.FirstName, " ",
                m.From.LastName, "[", m.From.Username, "]",
                " => ", m.Text, " triger=", m.Entities != null && m.Text.Contains(Information.NameBot)));


        }

        //public static void Main(string[] args)
        //{
        //    Initialization(null);
        //    while (true)
        //    {
        //        // Console.WriteLine("I'm a live");
        //        var i = 0;
        //        Thread.Sleep(5000);
        //    }
        //}

        public static void Write(string from, string text)
        {
            _Client.SendTextMessageAsync(new ChatId(-265678965), $"{from} ->  {text}");
        }
    }
}


//    public static void Main(string[] args)
//    {

//        new Thread(() => write()).Start();
//        var config = new ConfigurationBuilder().AddEnvironmentVariables("").Build();
//        url = config["ASPNETCORE_URLS"] ?? "http://*:8080";
//        BuildWebHost(args).Run();
//    }
//    static void  write()
//    {
//        while (true)
//        {
//            Thread.Sleep(4000);
//            Console.WriteLine(DateTime.Now.ToString("g"));
//        }
//    }

//    private static string url;
//    public static IWebHost BuildWebHost(string[] args) =>
//        WebHost.CreateDefaultBuilder(args)
//            .UseStartup<Startup>()
//            .UseUrls(url)
//            .Build();
//}