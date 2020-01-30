
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using a2_s3673712_s3719368.Data;
using a2_s3673712_s3719368.Models;
using Microsoft.AspNetCore.Http;
using a2_s3673712_s3719368.Attributes;
using X.PagedList;

namespace a2_s3673712_s3719368.Controllers
{
  
    [AuthorizeCustomer]
    public class CustomersController : Controller
    {
        private readonly NationBankContext _context;
        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value; //get CustomerID from session
        private int pageSize;

        public CustomersController(NationBankContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var customer = await _context.Customers.FindAsync(CustomerID); //find customer by CustomerID
            return View(customer);//return the target customer to the view
        }


        public async Task<IActionResult> History(int? id, int? page = 1)
        {
            Account account = await _context.Accounts.FindAsync(id);
            Customer customer = await _context.Customers.FindAsync(CustomerID);
            if (!customer.Accounts.Contains(account)) {//check if the customer own this account
                return NotFound();
            }
            ViewBag.AccountNumber = id;
            

            pageSize = 4; //only 4 transactions per page
             var pagedList = await _context.Transactions.Where(x => x.AccountNumber == account.AccountNumber).
                ToPagedListAsync(page, pageSize);

            return View(pagedList);
        }

       
    }
}
