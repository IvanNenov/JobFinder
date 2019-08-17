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
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace JobFinder.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            if (jobService == null)
            {
                throw new ArgumentNullException(nameof(jobService));
            }
            this._jobService = jobService;
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
                this._jobService.CreateJob(model, companyModel);
                return Redirect("/");
            }

            return this.View();
        }
    }
}
