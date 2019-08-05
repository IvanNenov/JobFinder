using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using JobFinder.Data;
using JobFinder.Models;
using JobFinder.Services.Contracts;
using JobFinder.ViewModels.InputViewModels;
using JobFinder.ViewModels.OutputViewModels;

namespace JobFinder.Services
{
    public class AdminService : IAdminService
    {
        private readonly JobDbContext _context;

        public AdminService(JobDbContext context)
        {
            _context = context;
        }
        public UpdateViewModel Update(string id)
        {
            var currentJob = this._context.JobAdds.Find(id);

            var model = new UpdateViewModel
            {
                Description = currentJob.Description,
                JobTitle = currentJob.JobTitle,
                JobType = currentJob.JobType.Value,
                Location = currentJob.Location,
                Salary = currentJob.Salary
            };

            return model;
        }

        public void Delete(string jobAddId)
        {
            if (jobAddId != null)
            {
                var jobAdd = this._context.JobAdds.Find(jobAddId);

                this._context.Remove(jobAdd);
                this._context.SaveChanges();
            }
        }

        public void EditedModel(UpdateViewModel model, string id)
        {
            var currentJob = this._context.JobAdds.Find(id);
            currentJob.JobType = model.JobType;
            currentJob.JobTitle = model.JobTitle;
            currentJob.Description = model.Description;
            currentJob.Location = model.Location;
            currentJob.Salary = model.Salary;
          
            this._context.SaveChanges();
        }
    }
}
