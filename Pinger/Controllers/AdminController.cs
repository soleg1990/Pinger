using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pinger.DAL;
using Pinger.Extensions;
using Pinger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pinger.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        IWebRepository repository;

        public AdminController(IWebRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Admin()
        {
            return View();
        }

        public JsonResult Sites()
        {
            var sites = repository.GetSites();
            var viewModel = sites.MapToAdminViewModel();
            return Json(viewModel);
        }

        [HttpPost]
        public async Task<JsonResult> Create([FromBody]Site site)
        {
            var newSite = await repository.CreateAsync(site.MapToDto());
            return Json(newSite);
        }

        [HttpPut]
        public async Task<bool> Edit([FromBody] Site site)
        {
            return await repository.UpdateAsync(site.MapToDto());
        }

        public async Task<bool> Delete(Guid id)
        {
            return await repository.DeleteAsync(id);
        }
    }
}
