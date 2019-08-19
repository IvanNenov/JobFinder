using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobFinder.Models;
using JobFinder.ViewModels.OutputViewModels;

namespace JobFinder.Services.Contracts
{
    public interface IUserService
    {
        bool TryAddToFavorite(string id);

        IEnumerable<FavJobsDto> GetFavoriteJobs();

        IEnumerable<AppliedJobOutputViewModel> GetAppliedJobs();

        bool TryAddToApplied(string id);

        void DeleteFromFavorite(string jobAddId);

        void DeleteFromApplied(string jobAddId);
    }
}
