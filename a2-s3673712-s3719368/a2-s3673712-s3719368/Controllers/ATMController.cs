
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using a2_s3673712_s3719368.Data;
using a2_s3673712_s3719368.Models;
using a2_s3673712_s3719368.Attributes;
using Microsoft.AspNetCore.Http;
using a2_s3673712_s3719368.LogicManger;

namespace a2_s3673712_s3719368.Controllers
{
    [Route("Bank/[controller]")]
    [AuthorizeCustomer]
    public class ATMController : Controller
    {
        private readonly NationBankContext _context;
        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value; //get CustomerID from session
        private List<Account> accountList;
        private TransferManger manger { get; set; }

        public ATMController(NationBankContext context)
        {
            _context = context;
        }

        public TransferManger GetManger()
        {
            if (manger == null)
            {
                manger = new TransferManger(_context, this, CustomerID);
            }
            return manger;
        }

        // GET: ATM
        public async Task<IActionResult> Transfer()
        {
            var customer = await _context.Customers.FindAsync(CustomerID); //find customer by CustomerID
            accountList = _context.Accounts.ToList();
            ViewBag.accountList = accountList;
            return View(customer);//return the target customer to the view
        }

     

        [HttpPost]
        public async Task<IActionResult> Transfer(string TransactionType, int FromAccount, int ToAccount, decimal amount, string Comment)
        {
            var customer = await _context.Customers.FindAsync(CustomerID);
            TransferManger manger = GetManger();
            accountList = _context.Accounts.ToList();
            manger.WithDrawOrDepositCheck(FromAccount,amount);
            if (!ModelState.IsValid)
            {
                ViewBag.Amount = amount;
                ViewBag.accountList = _context.Accounts.ToList();
                return View(customer);
            }
            switch (TransactionType) 
            {
                case "D":
                    if (await manger.Deposit(FromAccount, amount))
                     return RedirectToAction("Index", "Customers");
                    break;
                case "W":
                    if (await manger.Withdraw(FromAccount, amount))
                    {
                        return RedirectToAction("Index", "Customers");
                    }
                    else
                    {
                        ModelState.AddModelError("WithDrawFailed", "Withdraw failed, please check the amount and your account, and try again.");
                        ViewBag.Amount = amount;
                        ViewBag.accountList = _context.Accounts.ToList();
                        return View(customer);
                    }
                case "T":
                    if (await manger.Transfer(FromAccount, ToAccount, amount, Comment))
                    {
                        return RedirectToAction("Index", "Customers");
                    }
                    else
                    {
                        ModelState.AddModelError("WithDrawFailed", "Transfer failed, please check the amount and your account, and try again.");
                        ViewBag.Amount = amount;
                        ViewBag.accountList = _context.Accounts.ToList();
                        return View(customer);
                    }
                default:
                    ModelState.AddModelError("WithDrawFailed", "Transfer failed, please try to refresh the page.");
                    ViewBag.Amount = amount;
                    ViewBag.accountList = _context.Accounts.ToList();
                    return View(customer);
            }


            return RedirectToAction("Index","Customers");
            
        }

    }
}
