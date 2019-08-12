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
    public class CvController : Controller
    {
        private readonly ICvService _cvService;

        public CvController(IJobService jobService, ICvService cvService)
        {
            this._cvService = cvService;
        }

        [Authorize()]
        public IActionResult CreateCv()
        {
                if (this._cvService.HasCv())
                {
                    var model = this._cvService.GetMyCv();
                    return this.View("UserCv", model);
                }

                return this.View();
        }

        [Authorize()]
        [HttpPost]
        public IActionResult CreateCv(CvInputViewModel model)
        {
            if (ModelState.IsValid)
            {
                this._cvService.CreateCv(model);
                return this.Redirect("/");
            }

            return this.View();
        }

        [Authorize()]
        public IActionResult UpdateCv(string id)
        {
            var model = this._cvService.Update(id);
            return this.View(model);
        }

        [Authorize()]
        [HttpPost]
        public IActionResult UpdateCv(UpdateCvViewModel model, string id)
        {
            if (ModelState.IsValid)
            {
                this._cvService.EditedModel(model, id);
                return this.Redirect("../../Cv/CreateCv");
            }
            return this.View();
        }
    }
}
