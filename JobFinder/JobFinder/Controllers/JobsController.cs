using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobFinder.Services.Contracts;
using JobFinder.ViewModels.InputViewModels;
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
        public IActionResult PostJob()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> PostJob(PostJobInputModel model)
        {
            //if (ModelState.IsValid)
            //{
                this.jobService.CreateJob(model);
                return Redirect("/");
            //}

            //return this.View();
        }
    }
}
