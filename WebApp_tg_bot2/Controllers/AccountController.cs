using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp_tg_bot2.Models.Account;

namespace WebApp_tg_bot2.Controllers
{
    public class AccountController : Controller
    {
        private AccountContext db;
        public AccountController()
        {
            db = new AccountContext();
        }
        [Authorize]
        public IActionResult Index2()
        {
            return View("Index");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        //public IActionResult LoginNew()
        //{
        //    return View();
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserAccount user =
                    db.UserAccounts.FirstOrDefault(t => t.Login == model.Login && t.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Login);
                    return RedirectToAction("ChatView", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Некорректные лошиг и(или( пароль");
                }

            }

            return View(model);
        }

        async Task Authenticate(string login)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, login)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "AppCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}