using System;
using System.Collections.Generic;
using System.Text;

namespace JobFinder.Models
{
    public class Company
    {
        public Company()
        {
            this.JobAdds =  new HashSet<JobAdd>();
        }
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public virtual ICollection<JobAdd> JobAdds { get; set; }
    }
}
