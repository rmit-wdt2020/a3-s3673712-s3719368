using System;
using System.Linq;
using System.Threading.Tasks;
using a2_s3673712_s3719368.Attributes;
using a2_s3673712_s3719368.Data;
using a2_s3673712_s3719368.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace a2_s3673712_s3719368.Controllers
{
    [Area("default")]
    [AuthorizeCustomer]
    public class BillPaysController : Controller
    {
        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value; //get CustomerID from session
        private readonly NationBankContext _context;
        private int pageSize;
        private PaymentManger manager;
        private PaymentManger Manager
        {
            get
            {
                if (manager == null)
                {
                    manager = new PaymentManger(_context, this, CustomerID);
                }
                return manager;
            }
        }

        public BillPaysController(NationBankContext context)
        {
            _context = context;
        }

        //go to Billpays/Index
        public async Task<IActionResult> Index()
        {
            var customer = await _context.Customers.FindAsync(CustomerID); //find customer by CustomerID
            return View(customer);//return the target customer to the view
        }

        //Hide the logics


        public async Task<IActionResult> ListPayment(int? id, int? page = 1)
        {

            Account account = await _context.Accounts.FindAsync(id);
            if (!(await Manager.CheckNull(id)))
            {//check if the customer own this account
                return NotFound();
            }
            ViewBag.AccountNumber = id;


            pageSize = 4; //only 4 transactions per page
            var pagedList = await _context.BillPays.Where(x => x.AccountNumber == account.AccountNumber).
               ToPagedListAsync(page, pageSize);

            return View(pagedList);
        }

        public async Task<IActionResult> AddPayment(int? id)
        {

            if (!await Manager.CheckNull(id))
            {//check if the customer own this account
                return NotFound();
            }

            ViewBag.Payees = _context.Payees.ToList();
            return View(await _context.Accounts.FindAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddPayment(int? id, int PayeeID, decimal Amount, DateTime ScheduleDate, Period Period)
        {

            Account account = await _context.Accounts.FindAsync(id);
            if (await Manager.InsertPayment(id, PayeeID, Amount, ScheduleDate, Period))
            {
                return RedirectToAction("Index", "BillPays");
            }
            else
            {
                ViewBag.Payees = _context.Payees.ToList();
                ViewBag.Amount = Amount;
                ModelState.AddModelError("AccountNotExsit", "Error happen when creating your payment, try to refresh your page");//if cannot insert, like type is invalid
                return View(account);
            }

        }

        public async Task<IActionResult> Delete(int? id)
        {

            if (await Manager.DeletePayment(id))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var billPay = await _context.BillPays.FindAsync(id);

            if (billPay == null)
            {
                return NotFound();
            }

            ViewData["PayeeID"] = new SelectList(_context.Payees, "PayeeID", "PayeeID", billPay.PayeeID);
            ViewBag.ModifyDate = DateTime.UtcNow;
            ViewBag.Payees = _context.Payees.ToList();
            return View(billPay);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BillPayID,AccountNumber,PayeeID,Amount,ScheduleDate,Period,ModifyDate")] BillPay billPay)
        {

            if (await Manager.EditPayment(id, billPay))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Amount = billPay.Amount;
                ViewBag.ModifyDate = DateTime.Today.ToUniversalTime();
                ViewBag.Payees = _context.Payees.ToList();
                ViewData["PayeeID"] = new SelectList(_context.Payees, "PayeeID", "PayeeID", billPay.PayeeID);
                return View(billPay);
            }
        }



    }
}