﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using a2_s3673712_s3719368.Areas.Admin.Controllers.Managers;
using Microsoft.AspNetCore.Mvc;
using a2_s3673712_s3719368.Attributes;
namespace a2_s3673712_s3719368.Areas.Admin.Controllers
{
    [Area("admin")]
    [AuthorizeAdmin]
    public class SchedulePayManagerController : Controller
    {
        private BillManager billmanager;

        public SchedulePayManagerController() {
            billmanager = new BillManager();
        }
  
        public async Task<IActionResult> List()
        {
            var bills = await billmanager.GetAllBillPays();
            return View(bills);
        }
    }
}