using ImageLikes.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ImageLikes.Web.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


    }
}