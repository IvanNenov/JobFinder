using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobFinder.Data;
using JobFinder.Models;
using JobFinder.Services.Contracts;
using JobFinder.ViewModels.InputViewModels;

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
                CreatedOn = DateTime.UtcNow,
            };

            this.context.JobAdds.Add(jobAdd);
            this.context.SaveChangesAsync();
        }
    }
}
