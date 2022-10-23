﻿using Microsoft.AspNet.Identity;
using OSL_B2.Inventory.Repository.DbContexts;
using OSL_B2.Inventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WholeSale.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly IAccountService _accountAdapter;

        public TestController(IAccountService accountAdapter)
        {
            _accountAdapter = accountAdapter;
        }
        // GET: Test
        public ActionResult Index()
        {
            _accountAdapter.CreateAsync(new ApplicationUser { Email = "armana@gmail.com", UserName= "armaan@gmail.com" }, "Arman.00");
            return View();
        }
    }
}