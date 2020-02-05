
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using a2_s3673712_s3719368.Data;
using a2_s3673712_s3719368.Models;
using SimpleHashing;
using Microsoft.AspNetCore.Http;
using a2_s3673712_s3719368.Attributes;
using System;

namespace a2_s3673712_s3719368.Controllers
{

    [Route("Bank/SecureLogin")]
    public class LoginsController : Controller
    {
        private readonly NationBankContext _context;

        public LoginsController(NationBankContext context)
        {
            _context = context;
        }
        //Go to Log in view (start of the application)
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32(nameof(Customer.CustomerID)) != null)
            {
                return RedirectToAction("Index", "Customers");
            }
            else
            {
                return View();
            }
        }

        //If login successful, go to Customers/Index
        [HttpPost]
        public async Task<IActionResult> Login(string loginID, string password)
        {
            var login = await _context.Logins.FindAsync(loginID);
            if (login.Lock == true) 
            {
                ModelState.AddModelError("LoginFailed", "Account is being locked, please try again after 1 min");
            }
            //Validation
            if (login == null || !PBKDF2.Verify(login.PasswordHash, password))
            {
                ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
                login.attempt += 1;

                if (login.attempt >= 3)//check if the attempt is up to 3
                {
                    login.Lock = true;
                    login.LockDate = DateTime.UtcNow;
                }
                await _context.SaveChangesAsync();
                return View(new Login { LoginID = loginID });
            }

            if (!ModelState.IsValid)
            {
                ViewBag.LoginID = loginID;
                return View();
            }

            // Login customer.
            login.attempt = 0;//reset the attempt
            HttpContext.Session.SetInt32(nameof(Customer.CustomerID), login.CustomerID);
            HttpContext.Session.SetString(nameof(Customer.Name), login.Customer.Name);

            return RedirectToAction("Index", "Customers");
        }


        //Go to Login view with session clear (log out)
        [AuthorizeCustomer]
        [Route("LogoutNow")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Logins");
        }



    }
}
