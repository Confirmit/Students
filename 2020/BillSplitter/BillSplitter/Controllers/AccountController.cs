﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BillSplitter.Data;
using BillSplitter.Models;
using BillSplitter.Models.ViewModels.LoginAndRegister;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    [Route("Account")]
    public class AccountController : BaseController
    {
        private SignInManager _signInManager;

        public AccountController(UnitOfWork db, SignInManager manager) : base(db)
        {
            _signInManager = manager;
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

            User user = Db.Users.GetByName("LoginProvider", model.Name);

            if (user == null)
                ModelState.AddModelError("Name", "No Such User");
            else if(!_signInManager.CheckPassword(user, model.Password))
                ModelState.AddModelError("Password", "Wrong Password");
            else
            {
                _signInManager.SingIn(user);
                return RedirectTo(returnUrl);
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
            if (!ModelState.IsValid)
                return View(model);

            User user = Db.Users.GetByName("LoginProvider", model.Name);

            if (user != null)
                ModelState.AddModelError("Name", "UserName is already used");
            else
            {
                User newUser = new User
                {
                    Name = model.Name,
                    GivenName = model.GivenName,
                    Surname = model.Surname,
                    Password = model.Password
                };

                _signInManager.CreateNewUser(newUser);
                _signInManager.SingIn(newUser);

                return RedirectTo(returnUrl);
            }

            return View(model);
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
            var externalUser = _signInManager.GetExternalUserInfo(scheme).Result;

            _signInManager.ExternalSignIn(externalUser, scheme);

            return RedirectTo(returnUrl);
        }

        [Route("LoginConfirm")]
        public IActionResult RedirectTo(string returnUrl)
        {
           if (returnUrl != null)
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Bills");
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
