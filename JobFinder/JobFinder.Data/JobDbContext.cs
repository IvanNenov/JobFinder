using System;
using System.Collections.Generic;
using System.Text;
using JobFinder.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data
{
    public class JobDbContext : IdentityDbContext<User>
    {
        public JobDbContext(DbContextOptions options)
          : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Cv> Cvs { get; set; }

        public DbSet<FormEntry> FormEntries { get; set; }

        public DbSet<JobAdd> JobAdds { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
