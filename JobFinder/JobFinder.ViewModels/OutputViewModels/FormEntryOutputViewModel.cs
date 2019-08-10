using System;
using System.Collections.Generic;
using System.Text;

namespace JobFinder.ViewModels.OutputViewModels
{
    public class FormEntryOutputViewModel
    {
        public string Id { get; set; }
        public string SenderName { get; set; }

        public string SenderEmail { get; set; }

        public string PhoneNumber { get; set; }

        public string Message { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
