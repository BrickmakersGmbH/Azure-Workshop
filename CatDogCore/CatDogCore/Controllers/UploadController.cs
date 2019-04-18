using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using CatDogCore.Models;
using CatDogCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatDogCore.Controllers
{
    [Authorize]
    public class UploadController : Controller
    {
        private readonly IBlobstorageService _blobStorageService;

        public UploadController(IBlobstorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        public async Task<IActionResult> Index()
        {
            var paths = await _blobStorageService.GetFiles("uploads");
            ViewData["Images"] = paths.ToArray();
            return View();
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            foreach (var formFile in files)
            {
                using (var stream = formFile.OpenReadStream())
                {
                    await _blobStorageService.AddFile("uploads", stream);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
