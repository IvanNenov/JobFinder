using System;
using System.Collections.Generic;
using System.Text;

namespace JobFinder.ViewModels.OutputViewModels
{
    public class ListOfAllJobs
    {
        public IEnumerable<AllJobsView> AllJobs { get; set; }
    }
}
