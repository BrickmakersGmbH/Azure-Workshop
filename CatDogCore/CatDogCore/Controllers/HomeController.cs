using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CatDogCore.Models;
using CatDogCore.Services;

namespace CatDogCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlobstorageService _blobStorageService;

        public HomeController(IBlobstorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        public async Task<IActionResult> Index()
        {

            var paths = await _blobStorageService.GetFiles("uploads");
            ViewData["Images"] = paths.ToArray();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
