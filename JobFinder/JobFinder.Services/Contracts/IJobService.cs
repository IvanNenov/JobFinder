using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobFinder.Models;
using JobFinder.ViewModels.InputViewModels;
using JobFinder.ViewModels.OutputViewModels;

namespace JobFinder.Services.Contracts
{
    public interface IJobService
    {
        void CreateJob(PostJobInputModel model, CompanyInputViewModel companyModel);

        IQueryable<AllJobsView> AllJobs();

        IQueryable<SearchJobOutputViewModel> SearchForJob(ListOfAllJobs model);
    }
}
