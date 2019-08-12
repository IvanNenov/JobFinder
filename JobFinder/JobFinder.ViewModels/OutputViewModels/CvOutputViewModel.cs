using System;
using System.Collections.Generic;
using System.Text;
using JobFinder.Models;

namespace JobFinder.ViewModels.OutputViewModels
{
    public class CvOutputViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string Email { get; set; }
    }
}
