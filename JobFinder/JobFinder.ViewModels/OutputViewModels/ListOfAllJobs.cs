﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using JobFinder.Models;
using JobFinder.ViewModels.InputViewModels;

namespace JobFinder.ViewModels.OutputViewModels
{
    public class ListOfAllJobs
    {
        public IEnumerable<AllJobsView> AllJobs { get; set; }

        public IEnumerable<SearchJobOutputViewModel> SearchOutput { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public JobType? JobType { get; set; }
    }
}
