using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JobFinder.Services.Contracts;
using JobFinder.ViewModels.InputViewModels;
using JobFinder.ViewModels.OutputViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobService jobService;
        private readonly ICvService cvService;

        public JobsController(IJobService jobService, ICvService cvService)
        {
            this.jobService = jobService;
            this.cvService = cvService;
        }

        [Authorize()]
        public IActionResult PostJob()
        {
            return this.View();
        }

        [Authorize()]
        [HttpPost]
        public IActionResult PostJob(PostJobInputModel model, CompanyInputViewModel companyModel)
        {
            if (ModelState.IsValid)
            {
                this.jobService.CreateJob(model, companyModel);
                return Redirect("/");
            }

            return this.View();
        }

        [Authorize()]
        public IActionResult CreateCv()
        {
            if (this.cvService.HasCv())
            {
                var model =  this.cvService.GetMyCv();
                return this.View("UserCv", model);
            }

            return this.View();
        }

        [Authorize()]
        [HttpPost]
        public IActionResult CreateCv(CvInputViewModel model)
        {
            this.cvService.CreateCv(model);
            return this.Redirect("/");
        }
    }
}
