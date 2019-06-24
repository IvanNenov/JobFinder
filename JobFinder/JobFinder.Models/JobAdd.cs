using System;
using System.Collections.Generic;
using System.Text;

namespace JobFinder.Models
{
    public class JobAdd
    {
        public JobAdd()
        {
            this.CandidatesForPosition = new HashSet<UserJobAdds>();
        }
        public string Id { get; set; }

        public string Description { get; set; }

        public decimal? Salary { get; set; }

        public JobType? JobType { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<UserJobAdds> CandidatesForPosition { get; set; }

    }
}
