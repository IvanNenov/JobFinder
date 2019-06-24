using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JobFinder.Models
{
    public class FormEntry
    {
        public string Id { get; set; }
       
        public string SenderName { get; set; }

        public string SenderEmail { get; set; }

        public string PhoneNumber { get; set; }

        public string MessageBody { get; set; }
    }
}
