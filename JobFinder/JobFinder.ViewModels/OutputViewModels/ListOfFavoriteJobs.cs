using System;
using System.Collections.Generic;
using System.Text;
using JobFinder.Models;

namespace JobFinder.ViewModels.OutputViewModels
{
    public class ListOfFavoriteJobs
    {
        public IEnumerable<FavJobsDto> FavoriteJobsAds { get; set; }

        public IEnumerable<AppliedJobOutputViewModel> AppliedJobs { get; set; }
        public double TotalPagesCount { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }
    }
}
