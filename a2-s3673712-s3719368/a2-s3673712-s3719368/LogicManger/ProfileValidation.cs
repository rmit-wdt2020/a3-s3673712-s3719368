using a2_s3673712_s3719368.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleHashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace a2_s3673712_s3719368.LogicManger
{
    public class ProfileValidation
    {
        private readonly NationBankContext _context;
        private Controller controller;
        private int CustomerID;
        public ProfileValidation(NationBankContext context, Controller controller, int CustomerID)
        {
            _context = context;
            this.controller = controller;
            this.CustomerID = CustomerID;
        }
        public async void ValidateProfile(string state, string postcode, string phone)
        {
            var customer = await _context.Customers.FindAsync(CustomerID);
            if (customer == null)
            {
                controller.ModelState.AddModelError("NotFoundCustomer", "Customer Not Found");
            }
            bool isAustraliaState = false;

            String[] stateOfVictoria = { "VIC", "NSW", "QLD", "SA", "TAS", "WA" };
            foreach (String checkState in stateOfVictoria)
            {
                if (state.Equals(checkState)) { isAustraliaState = true; }
            }
            //Must be 3 lettered Australian state
            if (!Regex.IsMatch(state, @"^[A-Z]+$") || !isAustraliaState)
            {
                controller.ModelState.AddModelError("state", "State is invalid or not a Australia state.");
            }
            //Must be a 4 digit number
            if (!Regex.IsMatch(postcode, @"\b[0-9]{4}\b"))
            {
                controller.ModelState.AddModelError("postcode", "Postcode is invalid, Postcode format is 4 digit number.");
            }
            //Must be of the format: (61) - XXXX XXXX

            if (!Regex.IsMatch(phone, @"\b61[0-9]{8}\b"))
            {
                controller.ModelState.AddModelError("phone", "Phone is invalid, Phone format : 61XXXXXXXX.");
            }
        }
        public async void ValidatePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            var login = await _context.Logins.Include(x => x.Customer).FirstOrDefaultAsync(x => x.CustomerID == CustomerID);
            //Validation
            if (login == null)
            {
                controller.ModelState.AddModelError("NotFoundCustomer", "Customer Not Found");
            }
            //Check if old password is match
            if (!PBKDF2.Verify(login.PasswordHash, oldPassword))
            {
                controller.ModelState.AddModelError("oldPassword", "Old Password isn't match");
            }
            //check if old password is different from new password
            if (oldPassword == newPassword)
            {
                controller.ModelState.AddModelError("newPassword", "New Password cannot be the same with old Password");
            }
            //check if confirm password is match
            if (!(newPassword == confirmPassword))
            {
                controller.ModelState.AddModelError("confirmPassword", "Confirm Password isn't match");
            }
        }
    }
}
