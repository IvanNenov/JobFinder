using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobFinder.Services.Contracts;
using JobFinder.ViewModels.InputViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers
{
    public class ContactController : Controller
    {
        private readonly IFormEntryService formEntryService;

        public ContactController(IFormEntryService formEntryService)
        {
            this.formEntryService = formEntryService;
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Contact(FormEntryInputViewModel model)
        {
            if (ModelState.IsValid)
            {
                this.formEntryService.CreateFormEntry(model);
                return this.Redirect("/");
            }

            return this.View();
        }
    }
}
