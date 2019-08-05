using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobFinder.Models;
using JobFinder.Services.Contracts;
using JobFinder.ViewModels.OutputViewModels;
using Microsoft.AspNetCore.Authorization;

namespace JobFinder.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJobService _jobService;

        public HomeController(IJobService jobService)
        {
            this._jobService = jobService;
        }

        public IActionResult Index(int? currentPage, string searchTerm, string jobType)
        {
            ICollection<AllJobDto> allJobs = new List<AllJobDto>();
            
            var page = currentPage ?? 1;
            var pageSize = 5;
            var skip = (page - 1) * pageSize;

            double totalPageCount; 

            if (!string.IsNullOrEmpty(searchTerm) || !string.IsNullOrEmpty(jobType))
            {
                allJobs = this._jobService.SearchForJob(searchTerm, jobType)
                    .Skip(skip)
                    .Take(pageSize)
                    .OrderByDescending(x => x.CreatedOn)
                    .ToList();

                totalPageCount = Math.Ceiling((double)this._jobService.SearchForJob(searchTerm, jobType).Count() / pageSize);
            }
            else
            {
                allJobs =  this._jobService
                    .AllJobs()
                    .Skip(skip)
                    .Take(pageSize)
                    .OrderByDescending(x => x.CreatedOn)
                    .ToList();

                totalPageCount = Math.Ceiling((double)this._jobService.AllJobs().Count() / pageSize);
            }

            var viewModel = new ListOfAllJobs
            {
                AllJobs = allJobs,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPagesCount = totalPageCount
            };

            if (this.User.IsInRole("Admin"))
            {
                return this.View("AdminIndex", viewModel);
            }

            if (allJobs.Count > 0)
            {
                return this.View(viewModel);
            }

            return View(viewModel);
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
