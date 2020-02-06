using System.Text.RegularExpressions;
using System.Threading.Tasks;
using a2_s3673712_s3719368.Attributes;
using a2_s3673712_s3719368.Data;
using a2_s3673712_s3719368.LogicManger;
using a2_s3673712_s3719368.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleHashing;

namespace a2_s3673712_s3719368.Controllers
{
    [Area("default")]
    [AuthorizeCustomer]
    public class MyProfileController : Controller
    {
        private readonly NationBankContext _context;
        private ProfileValidation manager;
        private ProfileValidation Manager
        {
            get
            {
                if (manager == null)
                {
                    manager = new ProfileValidation(_context, this, CustomerID);
                }
                return manager;
            }
        }
        private int CustomerID  => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value ; //get CustomerID from session
      
        public MyProfileController(NationBankContext context)
        {
            _context = context;
        }

        //Go to view edit
        public async Task<IActionResult> Edit()
        {
            var customer = await _context.Customers.FindAsync(CustomerID); //find customer by CustomerID
            return View(customer);//return the target customer to the view
        }


        //Return to view Edit when submit form
        [HttpPost]
        public async Task<IActionResult> Edit(string name, string address, string city, string postcode, string state, string tfn, string phone)
        {
            var customer = await _context.Customers.FindAsync(CustomerID);
            //Validation

            Manager.ValidateProfile(state, postcode, phone);

            if (!ModelState.IsValid)
            {
                return View(customer);
            }
              
            customer.UpdateCustomer(name, address, city, postcode,  state, tfn, phone);//update to the database
            await _context.SaveChangesAsync(); //save changes

            return RedirectToAction(nameof(Edit));
        }
        //Go to ChangePassword view
        public async Task<IActionResult> ChangePassword()
        {

            var customer = await _context.Logins.FindAsync(CustomerID.ToString());
            return View(customer);//return the target customer to the view
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            var customer = await _context.Logins.FindAsync(CustomerID.ToString());
 
            var login = await _context.Logins.Include(x => x.Customer).FirstOrDefaultAsync(x => x.CustomerID == CustomerID);
            //Validation
            Manager.ValidatePassword(oldPassword, newPassword, confirmPassword);
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            login.UpdatePassword(PBKDF2.Hash(newPassword));
            await _context.SaveChangesAsync(); //save changes
            return RedirectToAction(nameof(PasswordChanged));
        }

        //go to PasswordChanged view
        public IActionResult PasswordChanged()
        {
            return View();
        }
    }
}