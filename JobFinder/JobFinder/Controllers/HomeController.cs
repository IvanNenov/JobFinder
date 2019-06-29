using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobFinder.Models;
using JobFinder.Services.Contracts;
using JobFinder.ViewModels.OutputViewModels;

namespace JobFinder.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJobService jobService;

        public HomeController(IJobService jobService)
        {
            this.jobService = jobService;
        }
        public IActionResult Index()
        {
            var listOfJobAdds = this.jobService.AllJobs().OrderByDescending(x => x.CreatedOn).ToList();

            if (listOfJobAdds.Count > 0)
            {
                return this.View(new ListOfAllJobs
                {
                    AllJobs = listOfJobAdds
                });
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
