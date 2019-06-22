using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace JobFinder.Models
{
    public class User : IdentityUser
    {
        public int Age { get; set; }
    }
}
