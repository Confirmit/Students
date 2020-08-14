﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    [Route("Account")]
    public class AccountController : BaseController
    {

        public AccountController(UnitOfWork db): base(db)
        {
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);
            User user =Db.Users.GetByName(model.Name);
            if (user == null)
                ModelState.AddModelError("Name", "No Such User");
            else
            {
                await Authenticate(user);

                if (returnUrl != null)
                    return Redirect(returnUrl);
                return RedirectToAction("Index", "Bills");
            }

            return View(model);
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Register(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);
            User user = Db.Users.GetByName(model.Name);

            if (user != null)
                ModelState.AddModelError("Name", "UserName is already used");
            else
            {
                User newUser = new User {Name = model.Name};
                Db.Users.Add(newUser);
             
                Db.Save();
                await Authenticate(newUser);
                if (returnUrl != null)
                    return Redirect(returnUrl);
                return RedirectToAction("Index", "Bills");
            }

            return View(model);
        }

        private async Task Authenticate(User user)
        {

            var claims = new List<Claim>
            {
                new Claim("Id",user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }

        [HttpPost]
        [Route("ExternalLogin")]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var callbackUrl = Url.Action("ExternalLoginCallback", new { scheme = provider, returnUrl });

            var props = new AuthenticationProperties
            {
                RedirectUri = callbackUrl
            };

            return new ChallengeResult(provider, props);
        }

        [Route("ExternalLoginCallback")]
        public async Task<IActionResult> ExternalLoginCallback(string scheme, string returnUrl)
        {
            var result = await HttpContext.AuthenticateAsync(scheme);
            if (result?.Succeeded != true)
                throw new Exception("External authentication error");

            var externalUser = result.Principal;
            if (externalUser == null)
                throw new Exception("External authentication error");

            var name = externalUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.GivenName).Value + ' ' + externalUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Surname).Value;

            await HttpContext.SignOutAsync("Cookies");
            
            User user = Db.Users.GetByName(name);

            if (user != null)
                return await Login(new LoginModel { Name = name }, returnUrl);

            return await Register(new RegisterModel { Name = name }, returnUrl);
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Bills");
        }
    }
}
