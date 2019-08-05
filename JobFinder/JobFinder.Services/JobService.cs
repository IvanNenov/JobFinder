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

        public IQueryable<AllJobDto> AllJobs()
        {
            var job = this.context.JobAdds.Select(x => new AllJobDto
            {
                Id = x.Id,
                Name = x.JobTitle,
                CompanyAddress = x.Location,
                CompanyName = x.Company.Name,
                JobType = x.JobType.Value,
                CreatedOn = x.CreatedOn
            });

            return job;
        }

        public IQueryable<AllJobDto> SearchForJob(string searchTerm, string jobType)
        {
            bool isJobTypeEnumParsed = Enum.TryParse(jobType, out JobType jobTypeEnum);
            
            if (searchTerm != null && jobType != null)
            {
                var listOfJobs = this.context.JobAdds.Where(x => x.JobTitle.Contains(searchTerm) && x.JobType == jobTypeEnum)
                    .Select(x => new AllJobDto
                    {
                        Name = x.JobTitle,
                        CompanyAddress = x.Company.Address,
                        CompanyName = x.Company.Name,
                        JobType = x.JobType.Value,
                        CreatedOn = x.CreatedOn
                    });

                return listOfJobs;
            }
            else if (searchTerm != null && !isJobTypeEnumParsed)
            {
                var listOfJobs = this.context.JobAdds.Where(x => x.JobTitle.Contains(searchTerm))
                    .Select(x => new AllJobDto
                    {
                        Name = x.JobTitle,
                        CompanyAddress = x.Company.Address,
                        CompanyName = x.Company.Name,
                        JobType = x.JobType.Value,
                        CreatedOn = x.CreatedOn
                    });

                return listOfJobs;
            }
            else if (searchTerm == null && isJobTypeEnumParsed)
            {
                var listOfJobs = this.context.JobAdds.Where(x => x.JobType == jobTypeEnum)
                    .Select(x => new AllJobDto
                    {
                        Name = x.JobTitle,
                        CompanyAddress = x.Company.Address,
                        CompanyName = x.Company.Name,
                        JobType = x.JobType.Value,
                        CreatedOn = x.CreatedOn
                    });

                return listOfJobs;
            }
            else
            {
                var listOfJobs = this.context.JobAdds.Select(x => new AllJobDto
                {
                    Name = x.JobTitle,
                    CompanyAddress = x.Company.Address,
                    CompanyName = x.Company.Name,
                    JobType = x.JobType.Value,
                    CreatedOn = x.CreatedOn
                });

                return listOfJobs;
            }

        }
    }
}
