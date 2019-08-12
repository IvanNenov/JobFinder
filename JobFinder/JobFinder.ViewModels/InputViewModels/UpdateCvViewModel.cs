using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JobFinder.ViewModels.InputViewModels
{
    public class UpdateCvViewModel
    {
        [MaxLength(100)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string ImageUrl { get; set; }
    }
}
