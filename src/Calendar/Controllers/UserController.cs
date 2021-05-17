using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Calendar.Services;
using Calendar.Models.SignOnViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;  /* .netcore 2.0 */
using System.Security.Claims;

namespace Calendar.Controllers
{
    //[Produces("application/json")]
    //[Route("api/Signing")]

    public class UserController : Controller
    {
        //private readonly Microsoft.AspNetCore.Authentication.IAuthenticationService _authService;
        //public UserController(Microsoft.AspNetCore.Authentication.IAuthenticationService authService)
        private readonly Calendar.Services.IAuthenticationService _authService;
        public UserController(Calendar.Services.IAuthenticationService authService)
        {
            _authService = authService;
        }

        //
        // GET: /User/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        //
        // POST: /User/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(SignOnViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var user = _authService.Login(model.Username, model.Password);
                    var user = new AppUser();
                    user.DisplayName = "Martin NG";
                    user.Username = "nww196";
                    user.IsAdmin = true;
                    
                    if (null != user)
                    {
                        var userClaims = new List<Claim>
                    {
                        new Claim("displayName", user.DisplayName),
                        new Claim("username", user.Username)
                    };
                        if (user.IsAdmin)
                        {
                            userClaims.Add(new Claim(ClaimTypes.Role, "Admins"));
                        }
                        var principal = new ClaimsPrincipal(new ClaimsIdentity(userClaims, _authService.GetType().Name));
                        /* .netore 2.0 begin */
                        //await HttpContext.Authentication.SignInAsync(
                        await HttpContext.SignInAsync(
                            "CalendarApp",
                            principal);
                        /* .netcore 2.0 end */
                        if (returnUrl == null)
                            return RedirectToAction("Calendar", "Events");
                        else
                            return Redirect(returnUrl);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        /*[Authorize(Roles = UserRoles.Everyone)]*/
        public async Task<IActionResult> Logout()
        {
            /* .netcore 2.0 begin */
            //await HttpContext.Authentication.SignOutAsync("CalendarApp");
            await HttpContext.SignOutAsync("CalendarApp");
            /* .netcore 2.0 end */
            return RedirectToAction("Calendar", "Events");
        }
    }
}
