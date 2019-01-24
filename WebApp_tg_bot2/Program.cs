using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebApp_tg_bot2
{
    public class Program
    {


        private static bool d;
        public static void Main(string[] args)
        {
            // PorterOfChat.Porter.Initialization(null);
            //   Dbg();
            string url_Web_Hook;

            string Token;
#if DEBUG
            Token = "521500060:AAH4Cj8XkwG0BpyDPy_a-hFN5LtFC5IC0sM"; //debug token
            url_Web_Hook = "https://be892e53.ngrok.io/home/update";
            
#else
            url_Web_Hook = "https://telegrambot228.herokuapp.com/home/update";
            Token = "568147661:AAHEsAzNZAbW-t_eJlOviuWHPBb8J81EHts";
#endif
            PorterOfChat.Porter.SetWebhook(Token,url_Web_Hook);
            CreateWebHostBuilder(args).Build().Run();


        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

    }


}
