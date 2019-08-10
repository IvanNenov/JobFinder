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
        private readonly IFormEntryService _formEntryService;

        public AdministrationController(IAdminService adminService, IFormEntryService formEntryService)
        {
            _adminService = adminService;
            _formEntryService = formEntryService;
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
        public IActionResult FormEntries(int? currentPage)
        {
            ICollection<FormEntryOutputViewModel> entries;

            var page = currentPage ?? 1;
            var pageSize = 5;
            var skip = (page - 1) * pageSize;

            entries = this._formEntryService
                .GetAll()
                .Skip(skip)
                .Take(pageSize)
                .OrderBy(x => x.CreatedOn)
                .ToList();

            double totalPageCount = Math.Ceiling((double)this._formEntryService.GetAll().Count() / pageSize);

            var viewModel = new ListOfAllEntries()
            {
               FormEntryOutput = entries,
               CurrentPage = page,
               PageSize = pageSize,
               TotalPagesCount = totalPageCount
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteFormEntry(string id)
        {
            this._formEntryService.DeleteFormEntry(id);
            return this.Redirect("/Administration/FormEntries");
        }
    }
}
