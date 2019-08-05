using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using JobFinder.Models;

namespace JobFinder.ViewModels.InputViewModels
{
    public class EditedNewModel
    {
        [MaxLength(50)]
        public string JobTitle { get; set; }

        public JobType JobType { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string Location { get; set; }

        public decimal? Salary { get; set; }
    }
}
