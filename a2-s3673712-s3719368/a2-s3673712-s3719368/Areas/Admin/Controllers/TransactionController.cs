using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using a2_s3673712_s3719368.Area.Admin.Models;
using a2_s3673712_s3719368.Areas.Admin.Controllers.Managers;
using a2_s3673712_s3719368.Areas.Admin.Models;
using a2_s3673712_s3719368.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace a2_s3673712_s3719368.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    public class TransactionController : Controller
    {
        private AccountManger accountManger;
        private TransactionManager transactionManager;
        public TransactionController() 
        {
            accountManger = new AccountManger();
            transactionManager = new TransactionManager();
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SelectTransaction() 
        {
            IEnumerable<AccountDto> accounts = await accountManger.GetAllAccounts();
            return View(accounts);
        }

        [HttpPost]
        public async Task<IActionResult> SelectTransaction(int? Account,DateTime FromDate, DateTime ToDate) 
        {
            IEnumerable<TransactionDto> transactions;
            if (ToDate > DateTime.UtcNow || FromDate > ToDate || FromDate > DateTime.UtcNow) 
            {
                ModelState.AddModelError("DateFailed", "Date Invalid.");
            }

            if (Account == 0)
            {
                transactions = await transactionManager.GetAllTransaction();
            }
            else {
                transactions = await transactionManager.GetTransaction(Account);
            }

            if (transactions == null) 
            {
                ModelState.AddModelError("DateFailed", "No transaction between these dates.");
            }

            if (!ModelState.IsValid) 
            {
                IEnumerable<AccountDto> accounts = await accountManger.GetAllAccounts();
                return View(accounts);
            }

            IEnumerable<TransactionDto> needed = transactionManager.FliterByDate(transactions, FromDate, ToDate);
            return View(nameof(List), needed);
        }

        public IActionResult List()
        {
            return View();
        }
    }
}