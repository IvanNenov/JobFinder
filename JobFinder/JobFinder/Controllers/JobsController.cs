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

        public JobsController(IJobService jobService)
        {
            this.jobService = jobService;
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
    }
}
