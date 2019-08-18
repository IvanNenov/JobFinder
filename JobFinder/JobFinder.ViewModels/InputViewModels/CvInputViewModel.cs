using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using JobFinder.Models;

namespace JobFinder.ViewModels.InputViewModels
{
    public  class CvInputViewModel
    {
        [MaxLength(100)]
        [Required]
        public string Description { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string ImageUrl { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
