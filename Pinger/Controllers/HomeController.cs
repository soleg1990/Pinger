using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pinger.DAL;
using Pinger.Extensions;
using Pinger.Models;

namespace Pinger.Controllers
{
    public class HomeController : Controller
    {
        IWebRepository repository;

        public HomeController(IWebRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Sites()
        {
            var sites = repository.GetSites();
            var viewModel = sites.MapToIndexViewModel();
            return Json(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
