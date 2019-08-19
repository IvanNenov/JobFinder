using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobFinder.Data;
using JobFinder.Models;
using JobFinder.Services;
using JobFinder.Services.Contracts;
using JobFinder.ViewModels.InputViewModels;
using JobFinder.ViewModels.OutputViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace JobFinder.Tests.Service
{
    public class JobServiceTests
    {
        private readonly IJobService _jobService;
        private readonly ICompanyService _companyService;
        private readonly JobDbContext _context;

        public JobServiceTests()
        {
            var options = new DbContextOptionsBuilder<JobDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new JobDbContext(options);
            SeedTestData(_context);
            this._jobService = new JobService(_context, _companyService);
            this._companyService = new CompanyService(_context);
        }

        [Fact]
        public void TestGetAllJobs_ShouldReturnAllJobs()
        {
            var expectedData = GetJobsTestData();
            var actualData = this._jobService.AllJobs();

            var jobTitleActual = actualData.Select(x => x.Name).ToList();
            var jobTitleExpected = actualData.Select(x => x.Name).ToList();


            Assert.Equal(expectedData.Count(), actualData.Count());
            Assert.Equal(jobTitleActual.Count(), jobTitleExpected.Count());
            Assert.Equal(jobTitleActual, jobTitleExpected);

        }

        [Fact]
        public void TestCreateJob_ShouldCreateJobWithCorrectParameters()
        {
            var expectedJob = ReturnCreatedJobTestData();

            var postJobInputViewModel = new PostJobInputModel
            {
                Company = "FB",
                Description = "AmazingJob",
                JobTitle = "Developer",
                JobType = JobType.FullTime,
                Location = "Nyc"
            };

            var companyInputModel = new CompanyInputViewModel()
            {
                Name = "FB",
                Address = "Nyc"
            };

            this._jobService.CreateJob(postJobInputViewModel, companyInputModel);
            this._context.SaveChanges();

            var actualJobTitle = this._context.JobAdds.FirstOrDefault(x => x.JobTitle == "Developer").JobTitle;
            var actualJobDescription = this._context.JobAdds.FirstOrDefault(x => x.Description == "AmazingJob").Description;

            Assert.Equal(actualJobTitle, expectedJob.JobTitle);
            Assert.Equal(actualJobDescription, expectedJob.Description);

        }

        [Fact]
        public void TestSearchForJob_ShouldReturnCorrectJobIfParameterIsOnlySearchTerm()
        {
            var job = new JobAdd()
            {
                JobTitle = "js",
                Description = "js",
                JobType = JobType.FullTime
            };

                this._context.JobAdds.Add(job);
            this._context.SaveChanges();

            string searchTerm = "js";

            var allJobs = this._jobService.SearchForJob(searchTerm, null).ToList();
            var currentJob = allJobs.FirstOrDefault(x => x.Name == searchTerm);

            var mappedJob = new JobAdd()
            {
                JobTitle = currentJob.Name,
                JobType = currentJob.JobType,
            };
            Assert.Equal(mappedJob.JobTitle,job.JobTitle);
        }

        [Fact]
        public void TestSearchForJob_ShouldReturnCorrectJobIfParameterIsOnlyJobType()
        {
            var job = new JobAdd()
            {
                JobTitle = "js",
                Description = "js",
                JobType = JobType.WorkOnShifts
            };

            this._context.JobAdds.Add(job);
            this._context.SaveChanges();

            string jobType = JobType.WorkOnShifts.ToString();

            var allJobs = this._jobService.SearchForJob(null, jobType).ToList();
            var currentJob = allJobs.FirstOrDefault(x => x.JobType == JobType.WorkOnShifts);

            var mappedJob = new JobAdd()
            {
                JobTitle = currentJob.Name,
                JobType = currentJob.JobType,
            };
            Assert.Equal(mappedJob.JobTitle, job.JobTitle);
        }

        [Fact]
        public void TestSearchForJob_ShouldReturnCorrectJobIfWeHaveDwoParameters()
        {
            var job = new JobAdd()
            {
                JobTitle = "php",
                Description = "js",
                JobType = JobType.PartTime
            };

            this._context.JobAdds.Add(job);
            this._context.SaveChanges();

            string jobType = JobType.PartTime.ToString();
            string searchTerm = "php";

            var allJobs = this._jobService.SearchForJob(searchTerm, jobType).ToList();
            var currentJob = allJobs.FirstOrDefault(x => x.JobType == JobType.PartTime);

            var mappedJob = new JobAdd()
            {
                JobTitle = currentJob.Name,
                JobType = currentJob.JobType,
            };
            Assert.Equal(mappedJob.JobTitle, job.JobTitle);
        }

        private void SeedTestData(JobDbContext context)
        {
            context.JobAdds.AddRange(GetJobsTestData());
            context.Companies.Add(new Company()
            {
                Address = "Nyc",
                Name = "FB"
            });
            context.SaveChanges();
        }

        private IEnumerable<JobAdd> GetJobsTestData()
        {
            var listOfJobs =  new List<AllJobDto>
            {
                new AllJobDto()
                {
                   JobType = JobType.FullTime,
                   CompanyAddress = "Sofia",
                   CompanyName = "Progress",
                   Name = ".Net"
                },

                new AllJobDto()
                {
                    JobType = JobType.PartTime,
                    CompanyAddress = "Sofia",
                    CompanyName = "Progress",
                    Name = "Js"
                }
            };

            var companyProgress = new Company()
            {
                Address = "Sofia",
                Name = "Progress"
            };

            var mappedJobs = listOfJobs.Select(x => new JobAdd()
            {
                JobTitle = x.Name,
                Company = companyProgress,
                JobType = x.JobType
            });

            return mappedJobs;
        }

        private JobAdd ReturnCreatedJobTestData()
        {
            var job = new JobAdd()
            {
                JobTitle = "Developer",
                Description = "AmazingJob"
            };

            return job;
        }
    }
}
