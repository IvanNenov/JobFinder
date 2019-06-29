using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using JobFinder.Data;
using JobFinder.Models;
using JobFinder.Services.Contracts;
using JobFinder.ViewModels.InputViewModels;
using JobFinder.ViewModels.OutputViewModels;

namespace JobFinder.Services
{
    public class JobService : IJobService
    {
        private readonly JobDbContext context;

        public JobService(JobDbContext context)
        {
            this.context = context;
        }
        public void CreateJob(PostJobInputModel model)
        {
            var company = this.context.Companies.FirstOrDefault(x => x.Name == model.Company);

            if (company == null)
            {
                return;
            }

            var jobAdd = new JobAdd
            {
                JobTitle = model.JobTitle,
                JobType = model.JobType,
                Salary = model.Salary,
                Company = company,
                Description = model.Description,
                Location = model.Location,
                CreatedOn = DateTime.UtcNow,
            };

            this.context.JobAdds.Add(jobAdd);
            this.context.SaveChanges();
        }
       
        public IQueryable<AllJobsView> AllJobs()
        {
            var job = this.context.JobAdds.Select(x => new AllJobsView
            {
                Name = x.JobTitle,
                CompanyAddress = x.Location,
                CompanyName = x.Company.Name,
                JobType = x.JobType.Value,
                CreatedOn = x.CreatedOn
            });

            return job;
        }
       public IQueryable<SearchJobOutputViewModel> SearchForJob(ListOfAllJobs model)
        {
            if (model.Name != null && model.JobType != null)
            {
                var listOfJobs = this.context.JobAdds.Where(x => x.JobTitle.Contains(model.Name) && x.JobType == model.JobType)
                    .Select(x => new SearchJobOutputViewModel
                    {
                        Name = x.JobTitle,
                        CompanyAddress = x.Company.Address,
                        CompanyName = x.Company.Name,
                        JobType = x.JobType
                    });

                return listOfJobs;
            }

            else if (model.Name != null && model.JobType == null)
            {
                var listOfJobs = this.context.JobAdds.Where(x => x.JobTitle.Contains(model.Name))
                    .Select(x => new SearchJobOutputViewModel
                    {
                        Name = x.JobTitle,
                        CompanyAddress = x.Company.Address,
                        CompanyName = x.Company.Name,
                        JobType = x.JobType
                    });

                return listOfJobs;
            }

            else if (model.Name == null && model.JobType != null)
            {
                var listOfJobs = this.context.JobAdds.Where(x => x.JobType == model.JobType)
                    .Select(x => new SearchJobOutputViewModel
                    {
                        Name = x.JobTitle,
                        CompanyAddress = x.Company.Address,
                        CompanyName = x.Company.Name,
                        JobType = x.JobType
                    });

                return listOfJobs;
            }

            else
            {
                var listOfJobs = this.context.JobAdds.Select(x => new SearchJobOutputViewModel()
                {
                    Name = x.JobTitle,
                    CompanyAddress = x.Company.Address,
                    CompanyName = x.Company.Name,
                    JobType = x.JobType
                });

                return listOfJobs;
            }
            
        }
    }
}
