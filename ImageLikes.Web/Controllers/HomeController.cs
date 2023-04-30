using ImageLikes.Data;
using ImageLikes.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Text.Json;

namespace ImageLikes.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;
        private IWebHostEnvironment _webHostEnvironment;
        public HomeController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult Index()
        {
            var repo = new Repository(_connectionString);
            var vm = new ViewModel
            {
                Images = repo.GetAll().OrderByDescending(i => i.DateUploaded).ToList()
            };

            return View(vm);
        }
        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Upload(IFormFile image, string title)
        {
            var fileName = $"{Guid.NewGuid()}-{image.FileName}";
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);
            using var fs = new FileStream(filePath, FileMode.CreateNew);
            image.CopyTo(fs);

            var repo = new Repository(_connectionString);
            var img = new Image
            {
                Title = title,
                FileName = fileName,
                Likes = 0,
                DateUploaded = DateTime.Now
            };
            repo.Add(img);
            return RedirectToAction("Index");
        }
        public IActionResult ViewImage(int id)
        {
            var repo = new Repository(_connectionString);
            var vm = new ImageViewModel
            {
                Image = repo.GetImageById(id)
            };
            if(vm.Image == null)
            {
                return RedirectToAction("Index");
            }
            List<int> idsInSession = HttpContext.Session.Get<List<int>>("idsInSession");
            if (idsInSession != null && idsInSession.Contains(vm.Image.Id))
            {
                vm.CanNotLike = true;
            }
            return View(vm);
        }
        public IActionResult GetImage(int id)
        {
            var repo = new Repository(_connectionString);
            var image = repo.GetImageById(id);
            if(image == null)
            {
                return RedirectToAction("Index");
            }
            return Json(image);
        }
        [HttpPost]
        public void UpdateLikes(int id)
        {
            var repo = new Repository(_connectionString);
            repo.UpdateLikes(id);
            var idsInSession = HttpContext.Session.Get<List<int>>("idsInSession");
            if(idsInSession == null)
            {
                idsInSession = new List<int>();
            }
            idsInSession.Add(id);
            HttpContext.Session.Set<List<int>>("idsInSession", idsInSession);
        }
    }
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default(T) :
                JsonSerializer.Deserialize<T>(value);
        }
    }
}