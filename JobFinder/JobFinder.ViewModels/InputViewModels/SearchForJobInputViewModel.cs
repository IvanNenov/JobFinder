using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using JobFinder.Models;

namespace JobFinder.ViewModels.InputViewModels
{
    public class SearchForJobInputViewModel
    {
        [MaxLength(50)]
        public string Name { get; set; }

        public JobType? JobType { get; set; }
        
        [MaxLength(50)]
        public string Location { get; set; }
    }
}
