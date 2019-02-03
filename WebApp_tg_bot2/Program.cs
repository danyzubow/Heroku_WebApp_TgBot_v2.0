using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebApp_tg_bot2
{
    public class Program
    {

        public static void Main(string[] args)
        {
            
            //XmlSerializer serial = new XmlSerializer(typeof(List<cChat>));
            //string PathXml = "E:\\ЛП\\С#\\_HEROKU\\WebApp_tg_bot2\\WebApp_tg_bot2\\Saves.xml";
            //#region File_to_db
            //Console.WriteLine("---------LoadData delete code!!!");
            //string text;
            //using (StreamReader wr = new StreamReader(PathXml))
            //{
            //    text = wr.ReadToEnd();
            //}
            //text = text.Replace("<Id>", "<Id_tg>");
            //text = text.Replace("</Id>", "</Id_tg>");
            //using (StreamWriter wr = new StreamWriter(PathXml, false))
            //{
            //    wr.Write(text);
            //}
            //#endregion

            //List<cChat> chats;
            //using (FileStream fs = new FileStream(PathXml, FileMode.Open))
            //{
            //    chats = (List<cChat>)serial.Deserialize(fs);
            //}
            //foreach (cChat group in chats)
            //{

            //    group.LockGroupPidor = false;
            //    group.LockGroupDad = false;
            //}

            //using (PorterDbContext chatdb = new PorterDbContext(false))
            //{
            //    foreach (var t in chats)
            //    {
            //        chatdb.chats.Add(t);
            //    }
            //    chatdb.SaveChanges();
            //}


            var connectionString = Environment.GetEnvironmentVariable("connectionString");
         
            string url_Web_Hook;
            string Token;
            if (connectionString == null)
            {
                throw new Exception("connectionString is empty");
            }

            PorterOfChat.Bot.Settings.ConnectionString = connectionString;
#if DEBUG

            #region WebHook_debug

            Token = "521500060:AAH4Cj8XkwG0BpyDPy_a-hFN5LtFC5IC0sM"; //debug token
            url_Web_Hook = "https://9b81e52f.ngrok.io/home/update";

            PorterOfChat.Porter.SetWebhook(Token, url_Web_Hook);
            #endregion

            #region Receiving

            //Token = "521500060:AAH4Cj8XkwG0BpyDPy_a-hFN5LtFC5IC0sM"; //debug token
            //string NameBot = "@PorterOfChatBot";
            //Porter _porter = new Porter(Token, NameBot);
            //_porter.StartReceiving(Token);
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
