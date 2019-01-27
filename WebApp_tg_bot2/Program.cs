using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using PorterOfChat;
using System;

namespace WebApp_tg_bot2
{
    public class Program
    {


        private static bool d;
        public static void Main(string[] args)
        {
            // PorterOfChat.Porter.Initialization(null);
            //   Dbg();
            //using (PorterDbContext db = new PorterDbContext())
            //{
            #region add
            //cChat q = new cChat(new List<cUser>()
            //{
            //    new cUser()
            //    {
            //        CountDad = "1",CountPidor = "2",FullName = "user1"
            //    }

            //}
            //    )
            //{ Name = "chat1" };



            //cChat w = new cChat(new List<cUser>()
            //        {
            //            new cUser()
            //            {
            //                CountDad = "1",CountPidor = "2",FullName = "user1"
            //            }

            //        }
            //    )
            //{ Name = "chat2" };

            //db.chats.Add(q);
            //db.chats.Add(w);

            #endregion

            //    cChat c1 = db.chats.Include(t => t.users).FirstOrDefault(t => t.Name == "chat1");
            //    cChat c12 = db.chats.Local.FirstOrDefault(t => t.Name == "chat1");
            //    cChat c2 = db.chats.Local.FirstOrDefault(t => t.Name == "chat2");
            //    db.SaveChanges();
            //}
            //return;


            string url_Web_Hook;

            string Token;
#if DEBUG

            #region WebHook_debug

            //Token = "521500060:AAH4Cj8XkwG0BpyDPy_a-hFN5LtFC5IC0sM"; //debug token
            //url_Web_Hook = "https://1bb515ba.ngrok.io/home/update";

            #endregion

            #region Receiving

            Token = "521500060:AAH4Cj8XkwG0BpyDPy_a-hFN5LtFC5IC0sM"; //debug token
            string NameBot = "@PorterOfChatBot";
            string ftpContactDll = $"{Environment.CurrentDirectory}\\wwwroot\\Data\\ftpcontact.dll";
            string Path = Environment.CurrentDirectory + "//";
            Porter _porter = new Porter(Token, NameBot, Path, "Saves.xml", ftpContactDll);
            _porter.StartReceiving(Token);
            #endregion

#else
            url_Web_Hook = "https://telegrambot228.herokuapp.com/home/update";
            Token = "568147661:AAHEsAzNZAbW-t_eJlOviuWHPBb8J81EHts";
            PorterOfChat.Porter.SetWebhook(Token,url_Web_Hook);
#endif
            CreateWebHostBuilder(args).Build().Run();



        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

    }


}
