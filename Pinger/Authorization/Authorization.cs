using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pinger.Authorization.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pinger.Authorization
{
    public class PingerAuthorization : IAuthorization
    {
        private AuthorizationContext context;

        public PingerAuthorization(AuthorizationContext context)
        {
            this.context = context;
        }

        public async Task<bool> Login(LoginModel model, HttpContext httpContext)
        {
            User user = await context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
            if (user != null)
            {
                await Authenticate(user, httpContext);
                return true;
            }
            return false;
        }

        private async Task Authenticate(User user, HttpContext httpContext)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
