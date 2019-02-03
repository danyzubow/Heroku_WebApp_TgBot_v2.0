using Microsoft.AspNetCore.Mvc;
using PorterOfChat;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WebApp_tg_bot2.Models;
namespace WebApp_tg_bot2.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            if (_porter == null)
            {

                string Token;
                string NameBot;

                //string Path;
                //string ftpContactDll;
#if DEBUG
                Token = "521500060:AAH4Cj8XkwG0BpyDPy_a-hFN5LtFC5IC0sM"; //debug token
                NameBot = "@PorterOfChatBot";

#else
                Token = "568147661:AAHEsAzNZAbW-t_eJlOviuWHPBb8J81EHts";
                NameBot = "@seadogs4_bot";
                
#endif
                _porter = new Porter(Token, NameBot);
            }
        }
        static PorterOfChat.Porter _porter;
        public IActionResult Index()
        {
            return RedirectToAction("ChatView", "Home");
        }
        [Authorize]
        public IActionResult ChatView()
        {

            return View(BaseControl.Data.GetMessageLogs().ToArray());
        }

        public OkResult update([FromBody]Update update)
        {
            //  return Ok();

            switch (update.Type)
            {
                case UpdateType.Message:

                    PorterOfChat.Porter.OnMessage(update.Message);
                    break;
                case UpdateType.CallbackQuery:


                    PorterOfChat.Porter.OnCallbackQuery(update.CallbackQuery);

                    break;
            }
            return Ok();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
