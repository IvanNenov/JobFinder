using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using JobFinder.Models;

namespace JobFinder.ViewModels.InputViewModels
{
    public class PostJobInputModel
    {
        [MaxLength(50)]
        public string JobTitle { get; set; }

        [MaxLength(50)]
        public string Company { get; set; }

        public JobType JobType { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        public decimal? Salary { get; set; }

    }
}
