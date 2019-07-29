﻿using System;
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
        private readonly ICompanyService _companyService;

        public JobService(JobDbContext context, ICompanyService companyService)
        {
            this.context = context;
            _companyService = companyService;
        }
        public void CreateJob(PostJobInputModel model, CompanyInputViewModel companyModel)
        {
            Company company = null;

            company = this.context.Companies.FirstOrDefault(x => x.Name == model.Company);

            if (company == null)
            {
                this._companyService.CreateCompany(companyModel, model);
                company = this.context.Companies.FirstOrDefault(x => x.Name == model.Company);
            }

            var jobAdd = new JobAdd
            {
                JobTitle = model.JobTitle,
                JobType = model.JobType,
                Salary = model.Salary,
                CompanyId = company.Id,
                Company = company,
                Description = model.Description,
                Location = model.Location,
                CreatedOn = DateTime.UtcNow,
            };

            var currentCompany = this.context.Companies.FirstOrDefault(x => x.Name == model.Company);
            this.context.JobAdds.Add(jobAdd);
            this.context.SaveChanges();
            if (currentCompany != null)
            {
                currentCompany.JobAdds.Add(jobAdd);
                this.context.SaveChanges();
            }

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
