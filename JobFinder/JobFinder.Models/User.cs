﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace JobFinder.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.UserJobAdds = new HashSet<UserJobAdds>();
            this.FavoriteJobs = new HashSet<FavoriteUserJobAds>();
        }

        public int Age { get; set; }

        public string CvId { get; set; }

        public virtual Cv Cv { get; set; }

        public virtual ICollection<FavoriteUserJobAds> FavoriteJobs { get; set; }

        public virtual ICollection<UserJobAdds> UserJobAdds { get; set; }
    }
}
