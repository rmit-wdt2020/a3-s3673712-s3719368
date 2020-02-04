using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using a2_s3673712_s3719368.Area.Admin.Models;
using a2_s3673712_s3719368.Areas.Admin.Controllers.Managers;
using a2_s3673712_s3719368.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace a2_s3673712_s3719368.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    public class TransactionController : Controller
    {
        private AccountManger accountManger;
        public TransactionController() 
        {
            accountManger = new AccountManger();
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
    }
}