using System.Diagnostics;
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
            ViewData["Title"] = "Uploads";
            return View();
        }

        public async Task<IActionResult> Cats()
        {
            var paths = await _blobStorageService.GetFiles("cats");
            ViewData["Images"] = paths.ToArray();
            ViewData["Title"] = "Cats";
            return View("Index");
        }

        public async Task<IActionResult> Dogs()
        {
            var paths = await _blobStorageService.GetFiles("dogs");
            ViewData["Images"] = paths.ToArray();
            ViewData["Title"] = "Dogs";
            return View("Index");
        }

        public async Task<IActionResult> Other()
        {
            var paths = await _blobStorageService.GetFiles("other");
            ViewData["Images"] = paths.ToArray();
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
