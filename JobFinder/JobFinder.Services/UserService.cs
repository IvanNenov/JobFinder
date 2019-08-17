using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobFinder.Data;
using JobFinder.Models;
using JobFinder.Services.Contracts;
using JobFinder.ViewModels.OutputViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Services
{
    public class UserService : IUserService
    {
        private readonly JobDbContext _context;
        private readonly IHttpContextAccessor _accessor;

        public UserService(JobDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }
        public bool TryAddToFavorite(string id)
        {
            var currentJob = this._context.JobAdds.Find(id);
            var currentUser = this._accessor.HttpContext.User.Identity.Name;
            var currentUserObject = this._context.Users.Include(x => x.FavoriteJobs).FirstOrDefault(x => x.UserName == currentUser);

            var isAppliedYet = currentUserObject.FavoriteJobs.FirstOrDefault(x => x.JobAddId == currentJob.Id);

            if (isAppliedYet != null)
            {
                return false;
            }

            var usersJobAdd = new FavoriteUserJobAds()
            {
                JobAdd = currentJob,
                JobAddId = currentJob.Id,
                User = currentUserObject,
                UserId = currentUserObject.Id
            };

            currentUserObject.FavoriteJobs.Add(usersJobAdd);
            currentJob.FavoriteUserJob.Add(usersJobAdd);

            this._context.SaveChanges();
            return true;
        }

        public IEnumerable<FavJobsDto> GetFavoriteJobs()
        {
            var currentUser = this._accessor.HttpContext.User.Identity.Name;
            var currentUserObject = this._context.Users.FirstOrDefault(x => x.UserName == currentUser);

            var jobAddsForCurrentUser = this._context.JobAdds
                .Include(x => x.FavoriteUserJob)
                .ThenInclude(x => x.JobAdd)
                .ThenInclude(x => x.Company)
                .Where(x => x.FavoriteUserJob.Any(y => y.UserId == currentUserObject.Id))
                .ToList();

            var appliedJobOutputViewModel = new List<FavJobsDto>();

            foreach (var jobAdd in jobAddsForCurrentUser)
            {
                var favoriteJobs = new FavJobsDto();

                favoriteJobs.JobType = jobAdd.JobType.Value;
                favoriteJobs.CompanyAddress = jobAdd.Company.Address;
                favoriteJobs.CompanyName = jobAdd.Company.Name;
                favoriteJobs.CreatedOn = jobAdd.CreatedOn;
                favoriteJobs.Name = jobAdd.JobTitle;

                appliedJobOutputViewModel.Add(favoriteJobs);
            }

            return appliedJobOutputViewModel;
        }

        public bool TryAddToApplied(string id)
        {
            var currentJob = this._context.JobAdds.Find(id);
            var currentUser = this._accessor.HttpContext.User.Identity.Name;
            var currentUserObject = this._context.Users.Include(x => x.UserJobAdds).FirstOrDefault(x => x.UserName == currentUser);

            var isAppliedYet = currentUserObject.UserJobAdds.FirstOrDefault(x => x.JobAddId == currentJob.Id);

            if (isAppliedYet != null)
            {
                return false;
            }

            var usersJobAdd = new UserJobAdds
            {
                JobAdd = currentJob,
                JobAddId = currentJob.Id,
                User = currentUserObject,
                UserId = currentUserObject.Id
            };

            currentUserObject.UserJobAdds.Add(usersJobAdd);
            currentJob.CandidatesForPosition.Add(usersJobAdd);

            this._context.SaveChanges();
            return true;
        }

        public IEnumerable<AppliedJobOutputViewModel> GetAppliedJobs()
        {
            var currentUser = this._accessor.HttpContext.User.Identity.Name;
            var currentUserObject = this._context.Users.FirstOrDefault(x => x.UserName == currentUser);

            var jobAddsForCurrentUser = this._context.JobAdds
                .Include(x => x.CandidatesForPosition)
                .ThenInclude(x => x.JobAdd)
                .ThenInclude(x => x.Company)
                .Where(x => x.CandidatesForPosition.Any(y => y.UserId == currentUserObject.Id))
                .ToList();

            List<AppliedJobOutputViewModel> appliedJobOutputViewModel = new List<AppliedJobOutputViewModel>();

            foreach (var jobAdd in jobAddsForCurrentUser)
            {
                AppliedJobOutputViewModel vmAppliedJobOutputViewModel = new AppliedJobOutputViewModel();

                vmAppliedJobOutputViewModel.JobType = jobAdd.JobType.Value;
                vmAppliedJobOutputViewModel.CompanyAddress = jobAdd.Company.Address;
                vmAppliedJobOutputViewModel.CompanyName = jobAdd.Company.Name;
                vmAppliedJobOutputViewModel.CreatedOn = jobAdd.CreatedOn;
                vmAppliedJobOutputViewModel.Name = jobAdd.JobTitle;

                appliedJobOutputViewModel.Add(vmAppliedJobOutputViewModel);
            }

            return appliedJobOutputViewModel;
        }
    }
}
