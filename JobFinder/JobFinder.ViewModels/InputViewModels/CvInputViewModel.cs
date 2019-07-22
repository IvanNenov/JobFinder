using System;
using System.Collections.Generic;
using System.Text;
using JobFinder.Models;

namespace JobFinder.ViewModels.InputViewModels
{
    public  class CvInputViewModel
    {
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
