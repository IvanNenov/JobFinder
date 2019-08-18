using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using JobFinder.Models;
using JobFinder.ViewModels.InputViewModels;

namespace JobFinder.ViewModels.OutputViewModels
{
    public class ListOfAllJobs
    {
        public IEnumerable<AllJobDto> AllJobs { get; set; }

        public double TotalPagesCount { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public IEnumerable<AllJobDto> SearchOutput { get; set; }

        [MaxLength(50)]
        public string SearchTerm { get; set; }

        public JobType? JobType { get; set; }

        public bool IsAny { get; set; }
    }
}
