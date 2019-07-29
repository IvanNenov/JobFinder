using System;
using System.Collections.Generic;
using System.Text;

namespace JobFinder.Models
{
    public class Cv
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
