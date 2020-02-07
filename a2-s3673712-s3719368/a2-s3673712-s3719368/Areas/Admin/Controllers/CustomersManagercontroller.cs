using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using a2_s3673712_s3719368.Areas.Admin.Models;
using Newtonsoft.Json;
using a2_s3673712_s3719368.Areas.Admin.Controllers.Managers;
using a2_s3673712_s3719368.Attributes;

namespace a2_s3673712_s3719368.Controllers
{
    [Area("admin")]
    [AuthorizeAdmin]
    public class CustomersManagercontroller : Controller
    {
        private CustomerManager customerManger;
        private LoginManager loginManger;

        public CustomersManagercontroller()
        {
            customerManger = new CustomerManager();
            loginManger = new LoginManager();
        }

        // GET: CustomersMange
        public async Task<IActionResult> Index()
        {
            var customers = await customerManger.GetAllCustomers();
            return View(customers);
        }

     


        // GET: CustomersMange/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await customerManger.GetCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: CustomersMange/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int CustomerID, CustomerDto customer)
        {
            if (CustomerID != customer.CustomerID)
                return NotFound();

            if (ModelState.IsValid)
            {
                if (customerManger.Edit(customer))
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(customer);
        }

        // GET: CustomersMange/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await customerManger.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: CustomersMange/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (customerManger.delete(id))
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound(); //error page here
        }

        // GET: CustomersMange/Lock/5
        public async Task<IActionResult> Lock(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await loginManger.GetLogin(id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        [HttpPost]
        public async Task<IActionResult> Lock(int id,int CustomerID)
        {
            var login = await loginManger.GetLogin(CustomerID);
            bool status;
            if (login.Lock == true)
            {
                status = false;
            }
            else {
                status = true;
            }

            if (loginManger.Lock(login, status)) 
            {
                return View(login);
            }

            return NotFound();
        }


    }
}
