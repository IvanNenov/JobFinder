using System;
using System.Collections.Generic;
using System.Text;
using JobFinder.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data
{
    public class JobDbContext : IdentityDbContext
    {
        public JobDbContext(DbContextOptions options)
          : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
