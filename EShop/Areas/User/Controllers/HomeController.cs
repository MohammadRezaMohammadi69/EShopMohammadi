﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Areas.User.Controllers
{
    [Area("User")]

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
  
    }
}
