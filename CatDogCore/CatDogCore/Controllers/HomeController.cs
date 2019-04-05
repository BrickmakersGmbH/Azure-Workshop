using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CatDogCore.Models;

namespace CatDogCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Images"] = new string[] {
                "https://cataas.com/cat/cute",
                "https://cataas.com/cat/cute",
                "https://cataas.com/cat/cute",
                "https://cataas.com/cat/cute",
                "https://cataas.com/cat/cute",
                "https://cataas.com/cat/cute",
                "https://cataas.com/cat/cute",
                "https://cataas.com/cat/cute",
                "https://cataas.com/cat/cute",
                "https://cataas.com/cat/cute",
                "https://cataas.com/cat/cute",
                "https://cataas.com/cat/cute",
                "https://cataas.com/cat/cute",
                "https://cataas.com/cat/cute",
                "https://cataas.com/cat/cute",
                "https://cataas.com/cat/cute",
                "https://cataas.com/cat/cute",
                "https://cataas.com/cat/cute"
            };

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
