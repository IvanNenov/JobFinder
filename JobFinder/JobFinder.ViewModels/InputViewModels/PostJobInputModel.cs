using System;
using System.Collections.Generic;
using System.Text;

namespace JobFinder.ViewModels.InputViewModels
{
    public class PostJobInputModel
    {
        public string JobTitle { get; set; }

        public string Company { get; set; }

        public int JobTypeEnum { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }
    }
}
