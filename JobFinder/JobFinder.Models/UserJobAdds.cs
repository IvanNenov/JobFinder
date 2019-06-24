using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace JobFinder.Models
{
    public class UserJobAdds
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string JobAddId { get; set; }
        public JobAdd JobAdd { get; set; }
    }
}
