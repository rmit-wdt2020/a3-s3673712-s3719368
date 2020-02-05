using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace a2_s3673712_s3719368.Controllers
{
    public class StatusCodeController : Controller
    {
        // GET: /<controller>/
        [HttpGet("/StatusCode/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            return View(statusCode);
        }
    }
}