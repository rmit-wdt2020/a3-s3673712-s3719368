﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using a2_s3673712_s3719368.Data;
using a2_s3673712_s3719368.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace a2_s3673712_s3719368.Controllers
{
    [Area("admin")]
   
    public class AdminController : Controller
    {
        [Route("Bank/SecureAdminLogin")]
        public IActionResult AdminLogin()
        {
            if (HttpContext.Session.GetString("Admin") != null)
            {
                return RedirectToAction("Index", "CustomersManager");
            }
            else
            {
                return View();
            }
        }
        [Route("Bank/SecureAdminLogin")]
        [HttpPost]
        public IActionResult AdminLogin(string LoginID, string password)
        {
            if (!(LoginID == "admin" && password == "admin")) 
            {
                ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.LoginID = LoginID;
                return View();
            }
            
            HttpContext.Session.SetString("Admin", LoginID);
            return RedirectToAction("Index", "CustomersManager");
        }

        public IActionResult AdminLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("AdminLogin", "Admin");
        }
        //Go to Error page if not log in
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}