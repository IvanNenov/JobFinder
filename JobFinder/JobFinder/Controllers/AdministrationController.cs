using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobFinder.Services.Contracts;
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
        public IActionResult Update()
        {
            return this.View();
        }

        public IActionResult Delete(string id)
        {
            this._adminService.Delete(id);
           return this.Redirect("/");
        }

        public IActionResult FormEntries()
        {
            return this.View();
        }
    }
}
