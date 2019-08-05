using System;
using System.Collections.Generic;
using System.Text;
using JobFinder.Models;
using JobFinder.ViewModels.InputViewModels;

namespace JobFinder.ViewModels.OutputViewModels
{
    public class UpdateViewModel
    {
        public string JobTitle { get; set; }

        public JobType JobType { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public decimal? Salary { get; set; }

      
    }
}
