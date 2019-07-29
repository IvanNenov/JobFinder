using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JobFinder.ViewModels.InputViewModels
{
    public class FormEntryInputViewModel
    {
        [MaxLength(50)]
        public string SenderName { get; set; }

       [EmailAddress]
        public string SenderEmail { get; set; }
       [MaxLength(10)]
        public string PhoneNumber { get; set; }


        [MaxLength(100)]
        public string Message { get; set; }
        
    }
}
