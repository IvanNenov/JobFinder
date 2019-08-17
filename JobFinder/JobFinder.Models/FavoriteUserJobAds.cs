using System;
using System.Collections.Generic;
using System.Text;

namespace JobFinder.Models
{
    public class FavoriteUserJobAds
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string JobAddId { get; set; }
        public JobAdd JobAdd { get; set; }
    }
}
