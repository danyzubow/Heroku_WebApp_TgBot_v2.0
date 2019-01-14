using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp_tg_bot2.Models;

using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
namespace WebApp_tg_bot2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
      //  [HttpPost]
        public OkResult update([FromBody]Update update)
        {
            cat.Program.Initialization(null);
          
            switch (update.Type)
            {
                case UpdateType.Message:
             
                    cat.Program.OnMessage(update.Message);
                    break;
                case UpdateType.CallbackQuery:
                  
               
                    cat.Program.OnCallbackQuery(update.CallbackQuery);

                    break;
            }
            return Ok();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
