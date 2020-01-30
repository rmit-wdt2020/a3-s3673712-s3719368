using System.Text.RegularExpressions;
using System.Threading.Tasks;
using a2_s3673712_s3719368.Attributes;
using a2_s3673712_s3719368.Data;
using a2_s3673712_s3719368.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleHashing;

namespace a2_s3673712_s3719368.Controllers
{
    [AuthorizeCustomer]
    public class MyProfileController : Controller
    {
        private readonly NationBankContext _context;


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
            
            if (customer == null) {
                ModelState.AddModelError("NotFoundCustomer", "Customer Not Found");
            }
   
            //Must be 3 lettered Australian state
            if (!Regex.IsMatch(state, @"\b^[A-Z]{3}\b"))
            {
                ModelState.AddModelError("state", "State is invalid, State format is 3 capital letter.");
            }
            //Must be a 4 digit number
            if (!Regex.IsMatch(postcode, @"\b[0-9]{4}\b"))
            {
                ModelState.AddModelError("postcode", "Postcode is invalid, Postcode format is 4 digit number.");
            }
            //Must be of the format: (61) - XXXX XXXX

            if (!Regex.IsMatch(phone, @"\b61[0-9]{8}\b"))
            {
                ModelState.AddModelError("phone", "Phone is invalid, Phone format : 61XXXXXXXX.");
            }

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
            //var logins =  _context.Logins.Where(c => c.CustomerID == CustomerID).ToList(); //Return list of Login that match customerID
            //var login = logins[0];
            var login = await _context.Logins.Include(x => x.Customer).FirstOrDefaultAsync(x => x.CustomerID == CustomerID);
            //Validation
            if (login == null)
            {
                ModelState.AddModelError("NotFoundCustomer", "Customer Not Found");
            }
            //Check if old password is match
            if (!PBKDF2.Verify(login.PasswordHash, oldPassword))
            {
                ModelState.AddModelError("oldPassword", "Old Password isn't match");
            }
            //check if old password is different from new password
            if (oldPassword == newPassword)
            {
                ModelState.AddModelError("newPassword", "New Password cannot be the same with old Password");
            }
            //check if confirm password is match
            if (!(newPassword == confirmPassword)) {
                ModelState.AddModelError("confirmPassword", "Confirm Password isn't match");
            }
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