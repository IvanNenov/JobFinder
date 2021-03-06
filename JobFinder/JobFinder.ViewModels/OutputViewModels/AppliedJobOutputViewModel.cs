﻿using System;
using System.Collections.Generic;
using System.Text;
using JobFinder.Models;

namespace JobFinder.ViewModels.OutputViewModels
{
    public class AppliedJobOutputViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public JobType JobType { get; set; }

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
