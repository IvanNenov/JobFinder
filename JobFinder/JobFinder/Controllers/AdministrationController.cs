using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobFinder.Services.Contracts;
using JobFinder.ViewModels.InputViewModels;
using JobFinder.ViewModels.OutputViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly IAdminService _adminService;

        public AdministrationController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Update(string id)
        {
            var model = this._adminService.Update(id);
            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(UpdateViewModel model, string id)
        {
            if (ModelState.IsValid)
            {
                this._adminService.EditedModel(model,id);
                return this.Redirect("/");
            }

            return this.View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            this._adminService.Delete(id);
           return this.Redirect("/");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult FormEntries()
        {
            return this.View();
        }
    }
}
