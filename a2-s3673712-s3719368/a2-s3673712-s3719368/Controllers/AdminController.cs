using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using a2_s3673712_s3719368.Data;
using Microsoft.AspNetCore.Mvc;

namespace a2_s3673712_s3719368.Controllers
{
    public class AdminController : Controller
    {
        private readonly NationBankContext _context;
        public AdminController(NationBankContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}