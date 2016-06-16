using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthorizationForoNetCore.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public async Task<IActionResult> Unauthorized(string returnUrl = null)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, "halower", ClaimValueTypes.String, "https://www.cnblogs.com/rohelm"));
            claims.Add(new Claim(ClaimTypes.Role, "Administrator", ClaimValueTypes.String, "https://www.cnblogs.com/rohelm"));
            claims.Add(new Claim("EmployeeNumber", "123456", ClaimValueTypes.String, "http://www.cnblogs.com/rohelm"));
            claims.Add(new Claim(ClaimTypes.DateOfBirth, "1900-01-01", ClaimValueTypes.Date, "http://www.cnblogs.com/rohelm"));
            //claims.Add(new Claim("UsernameAndPassword", "AAAXX12345678", ClaimValueTypes.String, "http://www.cnblogs.com/rohelm"));
            claims.Add(new Claim("AccessToken", DateTime.Now.AddMinutes(1).ToString(), ClaimValueTypes.String, "http://www.cnblogs.com/rohelm"));
            var userIdentity = new ClaimsIdentity("管理员");
            userIdentity.AddClaims(claims);
            var userPrincipal = new ClaimsPrincipal(userIdentity);
            await HttpContext.Authentication.SignInAsync("MyCookieMiddlewareInstance", userPrincipal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = false,
                    AllowRefresh = false
                });

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
