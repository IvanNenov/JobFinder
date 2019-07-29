using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JobFinder.Services.Contracts;
using JobFinder.ViewModels.InputViewModels;
using JobFinder.ViewModels.OutputViewModels;
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
        public IActionResult PostJob()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult PostJob(PostJobInputModel model, CompanyInputViewModel companyModel)
        {
            //if (ModelState.IsValid)
            //{
               this.jobService.CreateJob(model, companyModel);
                return Redirect("/");
            //}

            //return this.View();
        }

        [HttpPost]
        public IActionResult Search(ListOfAllJobs model)
        {
            var listOfJobs = this.jobService.SearchForJob(model);
            return this.View(new ListOfAllJobs()
            {
                SearchOutput = listOfJobs
            });
        }

        
        public IActionResult CreateCv()
        {
            if (this.cvService.HasCv())
            {
                var model =  this.cvService.GetMyCv();
                return this.View("UserCv", model);
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult CreateCv(CvInputViewModel model)
        {
            this.cvService.CreateCv(model);
            return this.Redirect("/");
        }
    }
}
