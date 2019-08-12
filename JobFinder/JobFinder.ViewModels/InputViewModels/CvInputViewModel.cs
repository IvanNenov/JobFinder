﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using JobFinder.Models;

namespace JobFinder.ViewModels.InputViewModels
{
    public  class CvInputViewModel
    {
        [MaxLength(100)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string ImageUrl { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
