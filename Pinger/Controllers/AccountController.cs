using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Pinger.Authorization;
using Pinger.Authorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pinger.Controllers
{
    public class AccountController : Controller
    {
        private IAuthorization authorization;

        public AccountController(IAuthorization authorization)
        {
            this.authorization = authorization;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (await authorization.Login(model, HttpContext))
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Некорректные логин и(или) пароль");

            return View(model);
        }
    }
}
