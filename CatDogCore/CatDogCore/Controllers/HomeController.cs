using System.Diagnostics;
using System.Threading.Tasks;
using CatDogCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using CatDogCore.Models;
using CatDogCore.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

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
            ViewData["Images"] = paths.Partition(3);
            ViewData["Title"] = "Uploads";
            return View();
        }

        public async Task<IActionResult> Cats()
        {
            var paths = await _blobStorageService.GetFiles("cats");
            ViewData["Images"] = paths.Partition(3);
            ViewData["Title"] = "Cats";
            return View("Index");
        }

        public async Task<IActionResult> Dogs()
        {
            var paths = await _blobStorageService.GetFiles("dogs");
            ViewData["Images"] = paths.Partition(3);
            ViewData["Title"] = "Dogs";
            return View("Index");
        }

        public async Task<IActionResult> Other()
        {
            var paths = await _blobStorageService.GetFiles("other");
            ViewData["Images"] = paths.Partition(3);
            ViewData["Title"] = "Other";
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
