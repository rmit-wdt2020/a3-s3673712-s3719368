using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using a2_s3673712_s3719368.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace a2_s3673712_s3719368.Controllers
{
    public class AdminController : Controller
    {
        private readonly NationBankContext _context;
        public AdminController(NationBankContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string LoginID, string password)
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
            return RedirectToAction("Index", "CustomersMange");
        }

        [Route("LogoutNow")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}